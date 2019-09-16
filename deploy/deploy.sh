#!/bin/bash

### Assumes that the script is called from ./hobbystacks/.

# exit when any command fails
set -e

# keep track of the last executed command
trap 'last_command=$current_command; current_command=$BASH_COMMAND' DEBUG
# echo an error message before exiting
trap 'echo "\"${last_command}\" command filed with exit code $?."' EXIT

logfile="deploy.log"

# Rename deployment files.
mkdir --parents data/nginx/
mv deploy/nginx.deploy.conf data/nginx/nginx.conf
mv deploy/docker-compose.deploy.yml docker-compose.yml

# Initialize SSL certificates.
if [ ! -d "data/certbot/conf/live/$4" ]
then
	chmod +x deploy/init-letsencrypt.sh
	./deploy/init-letsencrypt.sh -d $3 -e "$5" -p "$6" &>> $logfile
fi

# Prepare and launch Docker containers.
docker-compose -f docker-compose.yml down &>> $logfile
docker login -u $1 -p $2 xorcube.azurecr.io &>> $logfile
docker-compose -f docker-compose.yml pull &>> $logfile
docker logout xorcube.azurecr.io &>> $logfile
docker-compose -f docker-compose.yml up -d &>> $logfile

cat $logfile
rm $logfile
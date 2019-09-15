#!/bin/bash

# exit when any command fails
set -e

# keep track of the last executed command
trap 'last_command=$current_command; current_command=$BASH_COMMAND' DEBUG
# echo an error message before exiting
trap 'echo "\"${last_command}\" command filed with exit code $?."' EXIT

logfile="deploy.log"

# Rename deployment files.
mv hobbystacks/deploy/nginx.deploy.conf hobbystacks/data/nginx/nginx.conf
mv hobbystacks/deploy/docker-compose.deploy.yml hobbystacks/docker-compose.yml

docker-compose down &>> $logfile
docker login -u $1 -p $2 xorcube.azurecr.io &>> $logfile
docker-compose pull &>> $logfile
docker logout xorcube.azurecr.io &>> $logfile
docker-compose up -d &>> $logfile

cat $logfile
rm $logfile
#!/bin/bash

# exit when any command fails
set -e

# keep track of the last executed command
trap 'last_command=$current_command; current_command=$BASH_COMMAND' DEBUG
# echo an error message before exiting
trap 'echo "\"${last_command}\" command filed with exit code $?."' EXIT

logfile="deploy.log"

# Rename deployment files.
mkdir --parents hobbystacks/data/nginx/; mv hobbystacks/deploy/nginx.deploy.conf "${_}nginx.conf"
mv hobbystacks/deploy/docker-compose.deploy.yml hobbystacks/docker-compose.yml

docker-compose -f hobbystacks/docker-compose.yml down &>> $logfile
docker login -u $1 -p $2 xorcube.azurecr.io &>> $logfile
docker-compose -f hobbystacks/docker-compose.yml pull &>> $logfile
docker logout xorcube.azurecr.io &>> $logfile
docker-compose -f hobbystacks/docker-compose.yml up -d &>> $logfile

cat $logfile
rm $logfile
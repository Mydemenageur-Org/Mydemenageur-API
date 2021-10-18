#!/bin/bash

version=$1

# First we build the images
sudo docker build . -t  feldrise/mydemenageur-api 

# The we add tag for version
sudo docker image tag feldrise/mydemenageur-api:latest feldrise/mydemenageur-api:$version

# Finally we push to Docker hub
sudo docker push feldrise/mydemenageur-api:latest
sudo docker push feldrise/mydemenageur-api:$version
#!/bin/bash

# Removes the current containers
docker compose down

# Build the container
docker compose --env-file=.env build

# Start the containers
docker compose --env-file=.env up -d
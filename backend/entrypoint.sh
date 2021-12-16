#!/bin/bash

set -e
port=5000
run_cmd="dotnet run --server.urls http://*:" ${1-$port}

# Numbers of attemps to connect to the database
retry=5

# We assume we are connected
connected=true

# Attempt to connect to the database
until dotnet ef database update; do
	echo "Connecting to database..."
	retry=$((retry-1))
	echo "Retrying..." $retry
	sleep 1
	
    # If we have retried too many times, exit
	if [ $retry -eq 0 ]; then
		connected=false
		break
	fi
done

# If we were connected, run the server
if [ $connected = true ] 
then
	echo "Connected to database!"
	echo "Starting server..."
	exec $run_cmd
else
	echo "Cannot connect to database"
fi


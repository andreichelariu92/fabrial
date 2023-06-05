#!/bin/bash
docker compose -f ./dependencies/docker-compose.yml up -d
sleep 10s
docker stack deploy fabrial --compose-file docker-compose.yml
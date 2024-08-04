# rest-api-benchmark

Repo contains implementation same REST API using different languages and libraries in order to compare its performance.

Run test example:
```bash
docker-compose -f compose-dotnet-dapper.yaml up --force-recreate --abort-on-container-exit
docker-compose -f compose-dotnet-dapper.yaml down
```
NOTE: it's important to run `docker-compose down` to remove containers, otherwise result could be inaccurate.

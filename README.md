# rest-api-benchmark

Repo contains implementation same REST API using different languages and libraries in order to compare its performance.

Run test example, output to console:

```bash
# target=dapper|dapper-aot|go-gin|ef
./run.sh $target
```

Run test example, output to `output.txt`:

```bash
# target=dapper|dapper-aot|go-gin|ef
./run.sh $target -o output.txt
```

Run manually:

```bash
docker-compose -f compose-dotnet-dapper.yaml up --force-recreate --abort-on-container-exit
docker-compose -f compose-dotnet-dapper.yaml down
```

NOTE: it's important to run `docker-compose down` to remove containers, otherwise result could be inaccurate.
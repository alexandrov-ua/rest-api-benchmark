#!/usr/bin/env bash

set -e

print_usage() {
  echo "Usage: ${0} [dapper|dapper-aot|go-gin|ef] [options]"
  echo "  dapper      : dotnet+dapper benchmark"
  echo "  dapper-aot  : dotnet native AOT+dapper benchmark"
  echo "  go-gin      : go+Gin+sqlc benchmark"
  echo "  ef          : dotnet+Entity Framework Core benchmark"
  echo "-o <file_name>: redirect output to file"
}

if [[ -s "${1}" ]]; then
  print_usage
  exit 1
fi

OUT_NAME=""
if [[ "${2}" == "-o" ]]; then
  OUT_NAME="${3}"
fi

CMP_FILE=""
case "${1}" in
  dapper)
    CMP_FILE="compose-dotnet-dapper.yaml"
    ;;
  dapper-aot)
    CMP_FILE="compose-dotnet-dapper-aot.yaml"
    ;;
  go-gin)
    CMP_FILE="compose-go-gin-sqlc.yaml"
    ;;
  ef)
    CMP_FILE="compose-dotnet-ef.yaml"
    ;;
  *)
    print_usage
    exit 1
    ;;
esac

docker compose -f "${CMP_FILE}" build
if [[ -n "${OUT_NAME}" ]]; then
  echo "Writing output to ${OUT_NAME}"
  docker compose -f "${CMP_FILE}" up --force-recreate --abort-on-container-exit > "${OUT_NAME}"
else
  docker compose -f "${CMP_FILE}" up --force-recreate --abort-on-container-exit
fi
docker compose -f "${CMP_FILE}" down
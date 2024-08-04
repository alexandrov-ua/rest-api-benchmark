FROM mcr.microsoft.com/dotnet/sdk:8.0 as build-env
WORKDIR /build
RUN apt update && apt install -y clang zlib1g-dev
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release --self-contained true -o out

#FROM mcr.microsoft.com/dotnet/aspnet:8.0 
FROM ubuntu:22.04
RUN apt update && apt install -y curl
# Example: Host=localhost; Database=my-db; Username=postgres; Password=Qweasdzxc123; SSL Mode=Disable;
ENV ConnectionStrings__Default "" 
ENV ASPNETCORE_URLS "http://*:8080"
EXPOSE 8080
WORKDIR /App
COPY --from=build-env /build/out/ .
ENTRYPOINT ["./AuthorsDbRest.Dapper.Aot"]
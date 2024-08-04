FROM mcr.microsoft.com/dotnet/sdk:8.0 as build-env
WORKDIR /build
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 
RUN apt update && apt install -y curl
# Example: Host=localhost; Database=my-db; Username=postgres; Password=Qweasdzxc123; SSL Mode=Disable;
ENV ConnectionStrings__Default "" 
ENV ASPNETCORE_URLS "http://*:8080"
EXPOSE 8080
WORKDIR /App
COPY --from=build-env /build/out/ .
ENTRYPOINT ["dotnet", "AuthorsDbRest.Dapper.dll"]
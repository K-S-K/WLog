# Using of the service.
# 
# How to reuse locally cached origin image:
# 1. docker pull mcr.microsoft.com/dotnet/sdk:8.0
#    docker pull mcr.microsoft.com/dotnet/aspnet:8.0
# 2. docker tag mcr.microsoft.com/dotnet/sdk:8.0 local/dotnet-sdk:8.0
#    docker tag mcr.microsoft.com/dotnet/aspnet:8.0 local/dotnet-rtm:8.0
# 3. Use locally saved local/dotnet-rtm:8.0 as an origin image for build
#    Use locally saved local/dotnet-sdk:8.0 as an origin image for start
# 
# docker rm $(docker ps -a -q --filter "ancestor=wlog")
# docker rmi $(docker images  --filter "reference=wlog")
# 
# Creating of the service.
# The buidl command will be looking like this:
# docker build -t wlog:1.0 .
# 
# How to inspect docker content:
# docker run -it wlog:1.0 sh
# pwd
# ls -la
# 
# How to execute the docker image in the container:
# docker-compose up --force-recreate
# 
# Check responce: http://localhost:/

FROM local/dotnet-sdk:8.0 AS sdk
WORKDIR /app

COPY ./ ./
# RUN dotnet publish -c Release -o out

RUN dotnet publish WLog.Data/WLog.Data.csproj -c Release -o out/data
RUN dotnet publish WLog.API/WLog.API.csproj -c Release -o out/api
RUN dotnet publish WLog.Site/WLog.Site.csproj -c Release -o out/ui
RUN chmod +x /app/Execute.sh

FROM local/dotnet-rtm:8.0
WORKDIR /app
COPY --from=sdk /app/out/data /app/data
COPY --from=sdk /app/out/api /app/api
COPY --from=sdk /app/out/ui /app/ui
COPY --from=sdk /app/Execute.sh /app/Execute.sh

# Run multiple processes in a container
# https://docs.docker.com/config/containers/multi-service_container/
CMD /app/Execute.sh

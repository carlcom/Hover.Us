FROM microsoft/dotnet:1.0.0-preview2-sdk
RUN mkdir -p /app
WORKDIR /app
EXPOSE 5000

RUN apt-get update && apt-get dist-upgrade -y && apt-get install -y sqlite3 libsqlite3-dev
COPY . /app
RUN dotnet restore
RUN dotnet build

ENTRYPOINT ["dotnet", "run", "--server.urls=http://*:5000"]

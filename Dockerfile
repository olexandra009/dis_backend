FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /source

COPY *.sln .
COPY DIS_data/*.csproj ./DIS_data/
COPY DIS_Server/*.csproj ./DIS_Server/
RUN dotnet restore -r linux-x64

COPY DIS_data/. ./DIS_data/
COPY DIS_Server/. ./DIS_Server/
WORKDIR /source/DIS_Server
RUN dotnet publish -c release -o /app -r linux-x64 --self-contained false --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal-amd64
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["./DIS_Server"]
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY Mydemenageur.API/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY Mydemenageur.API/. ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app

COPY --from=build-env /app/out .
COPY Mydemenageur.API/Mydemenageur.API.xml ./

ENTRYPOINT ["dotnet", "Mydemenageur.API.dll"]
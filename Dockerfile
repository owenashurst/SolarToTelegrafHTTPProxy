# Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app
COPY . ./
RUN dotnet restore
RUN dotnet build
RUN dotnet publish --configuration Release -o build

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0
ARG ENVIRONMENT
ENV ASPNETCORE_ENVIRONMENT=${ENVIRONMENT}
WORKDIR /app
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "SolarToTelegrafHTTPProxy.dll"]

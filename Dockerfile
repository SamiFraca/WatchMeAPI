FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /WatchMEAPI

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /WatchMEAPI
COPY --from=build-env /WatchMEAPI/out .
ENTRYPOINT ["dotnet", "WatchME.dll"]
# ENTRYPOINT ["/bin/bash"]

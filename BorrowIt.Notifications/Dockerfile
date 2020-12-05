FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy everything else and build
COPY . ./

# Copy csproj and restore as distinct layers
RUN dotnet restore --configfile ./nuget.config ./BorrowIt.Notifications/BorrowIt.Notifications.csproj
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "BorrowIt.Notifications.dll"]
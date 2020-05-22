FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source
COPY . .

WORKDIR /source/DojoTracker
RUN dotnet restore DojoTracker.csproj
RUN dotnet add package Microsoft.EntityFrameworkCore.Analyzers
RUN dotnet publish -c release -o /app --no-restore DojoTracker.csproj
WORKDIR /source/DojoTrackerTest
RUN dotnet test

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "DojoTracker.dll"]
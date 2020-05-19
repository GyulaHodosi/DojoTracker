FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source/DojoTracker
RUN dotnet restore
RUN dotnet add package Microsoft.EntityFrameworkCore.Analyzers --version 3.1.3
RUN dotnet publish -c release -o /app --no-restore
RUN dotnet test

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "DojoTracker.dll"]
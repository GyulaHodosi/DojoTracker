FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source
COPY . .

RUN ls
RUN dotnet restore DojoTracker.csproj
RUN dotnet add package Microsoft.EntityFrameworkCore.Analyzers --version 3.1.3
RUN dotnet publish -c release -o /app --no-restore DojoTracer.csproj
RUN dotnet test

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "DojoTracker.dll"]
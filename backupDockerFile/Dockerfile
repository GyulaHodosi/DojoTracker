# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .

# COPY aspnetapp/*.csproj ./aspnetapp/
COPY *.csproj .
RUN dotnet restore





# copy everything else and build app
COPY . .
# WORKDIR /source



RUN dotnet add package Microsoft.EntityFrameworkCore.Analyzers --version 3.1.3
# RUN dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 3.1.3
# RUN dotnet add package Microsoft.EntityFrameworkCore.Tools --version 3.1.3
# RUN dotnet add package Microsoft.EntityFrameworkCore.Tools.DotNet --version 2.0.3
# RUN dotnet tool install --global dotnet-ef --version 3.1.3
# RUN dotnet --list-sdks


# RUN dotnet ef database update --version 3.1.3


RUN dotnet publish -c release -o /app --no-restore


# RUN dotnet ef database update

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
# FROM mcr.microsoft.com/dotnet/core/runtime:3.1
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "DojoTracker.dll"]
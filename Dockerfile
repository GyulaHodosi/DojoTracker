FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source
COPY . .

WORKDIR /source/DojoTracker
RUN dotnet restore DojoTracker.csproj
RUN dotnet add package Microsoft.EntityFrameworkCore.Analyzers --version 3.1.3
RUN curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.35.3/install.sh | bash
RUN export NVM_DIR="$HOME/.nvm"
RUN [ -s "$NVM_DIR/nvm.sh" ] && \. "$NVM_DIR/nvm.sh"
RUN [ -s "$NVM_DIR/bash_completion" ] && \. "$NVM_DIR/bash_completion"
RUN dotnet publish -c release -o /app --no-restore DojoTracker.csproj
WORKDIR /source/DojoTrackerTest
RUN dotnet test

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "DojoTracker.dll"]
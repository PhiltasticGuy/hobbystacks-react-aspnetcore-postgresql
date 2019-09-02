FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine3.9 AS build
WORKDIR /src
COPY . .
WORKDIR "/src/src/Services/Registrar/Registrar.Api"
RUN dotnet restore
RUN dotnet build --no-restore -c Release -o /app

FROM build as functionaltest
WORKDIR /src/src/Services/Registrar/Registrar.FunctionalTests
RUN dotnet test

FROM build as unittest
WORKDIR /src/src/Services/Registrar/Registrar.UnitTests
RUN dotnet test

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Registrar.Api.dll"]
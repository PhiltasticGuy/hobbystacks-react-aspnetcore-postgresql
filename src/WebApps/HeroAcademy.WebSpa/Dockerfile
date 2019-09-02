FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM node:alpine AS build-node
WORKDIR /ClientApp
COPY "/src/WebApps/HeroAcademy.WebSpa/ClientApp/package.json" .
COPY "/src/WebApps/HeroAcademy.WebSpa/ClientApp/package-lock.json" .
RUN npm install
COPY "/src/WebApps/HeroAcademy.WebSpa/ClientApp" .
RUN npm run build

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine3.9 AS build
ENV BuildingDocker true
WORKDIR /src
COPY . .
WORKDIR "/src/src/WebApps/HeroAcademy.WebSpa"
RUN dotnet restore
RUN dotnet build --no-restore -c Release -o /app

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
COPY --from=build-node /ClientApp/build ./ClientApp/build
ENTRYPOINT ["dotnet", "HeroAcademy.WebSpa.dll"]
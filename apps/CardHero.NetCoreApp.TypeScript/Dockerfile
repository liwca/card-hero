#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
COPY . .
CMD ASPNETCORE_URLS=http://*:$PORT CARD_HERO_ConnectionStrings__CardHeroPostgreSqlConnection=$DATABASE_URL dotnet CardHero.NetCoreApp.TypeScript.dll
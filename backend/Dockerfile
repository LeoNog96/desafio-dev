#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.


FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /build
COPY Cnab.Api.sln ./

COPY ["src/Cnab.Api.Domain/*.csproj", "src/Cnab.Api.Domain/"]
COPY ["src/Cnab.Api.Contracts/*.csproj", "src/Cnab.Api.Contracts/"]
COPY ["src/Cnab.Api.Data/*.csproj", "src/Cnab.Api.Data/"]
COPY ["src/Cnab.Api.Cache/*.csproj", "src/Cnab.Api.Cache/"]
COPY ["src/Cnab.Api.Logs/*.csproj", "src/Cnab.Api.Logs/"]
COPY ["src/Cnab.Api.Services/*.csproj", "src/Cnab.Api.Services/"]
COPY ["src/Cnab.Api.Application/*.csproj", "src/Cnab.Api.Application/"]
COPY ["src/Cnab.Api.Test/*.csproj", "src/Cnab.Api.Test/"]
COPY ["src/Cnab.Api.WebApi/Cnab.Api.WebApi.csproj", "src/Cnab.Api.WebApi/"]

RUN dotnet restore
COPY . .
#RUN dotnet test
WORKDIR /build/src/Cnab.Api.WebApi
RUN dotnet build "Cnab.Api.WebApi.csproj" -c Release -o /app

WORKDIR /build
FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
ENV TZ=America/Sao_Paulo
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "Cnab.Api.WebApi.dll"]
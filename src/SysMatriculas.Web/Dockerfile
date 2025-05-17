#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/SysMatriculas.Web/SysMatriculas.Web.csproj", "src/SysMatriculas.Web/"]
COPY ["src/SysMatriculas.Negocio/SysMatriculas.Negocio.csproj", "src/SysMatriculas.Negocio/"]
COPY ["src/SysMatriculas.Models/SysMatriculas.Dominio.csproj", "src/SysMatriculas.Models/"]
COPY ["src/SysMatriculas.Persistencia/SysMatriculas.Persistencia.csproj", "src/SysMatriculas.Persistencia/"]
RUN dotnet restore "./src/SysMatriculas.Web/SysMatriculas.Web.csproj"
COPY . .
WORKDIR "/src/src/SysMatriculas.Web"
RUN dotnet build "./SysMatriculas.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SysMatriculas.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SysMatriculas.Web.dll"]
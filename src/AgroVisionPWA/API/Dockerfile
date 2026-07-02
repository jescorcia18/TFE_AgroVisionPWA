# Esta fase se usa para la ejecución en modo rápido (Valor predeterminado para depuración)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
#USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Esta fase se usa para compilar todos los proyectos de la arquitectura limpia
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY . .
# 1. Copiar los archivos .csproj respetando la estructura real visible en tu imagen
#COPY ["src/AgroVisionPWA/API/CoffeePestDetection.API.csproj", "src/AgroVisionPWA/API/CoffeePestDetection.API/"]
#COPY ["src/AgroVisionPWA/Application/CoffeePestDetection.Application/CoffeePestDetection.Application.csproj", "src/AgroVisionPWA/Application/CoffeePestDetection.Application/"]
#COPY ["src/AgroVisionPWA/Domain/CoffeePestDetection.Domain/CoffeePestDetection.Domain.csproj", "src/AgroVisionPWA/Domain/CoffeePestDetection.Domain/"]
#COPY ["src/AgroVisionPWA/Infrastructure/CoffeePestDetection.Infrastructure/CoffeePestDetection.Infrastructure.csproj", "src/AgroVisionPWA/Infrastructure/CoffeePestDetection.Infrastructure/"]

# 2. Restaurar dependencias globales apuntando al proyecto API
RUN dotnet restore "src/AgroVisionPWA/API/CoffeePestDetection.API.csproj"
COPY . .

# 3. Moverse al directorio del API para compilar
WORKDIR "/src/src/AgroVisionPWA/API"
RUN dotnet build "CoffeePestDetection.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase publica los binarios optimizados eliminando herramientas de SDK
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CoffeePestDetection.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Fase final: Configuración del contenedor ligero de producción
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoffeePestDetection.API.dll"]

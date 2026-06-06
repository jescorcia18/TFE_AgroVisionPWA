#!/bin/bash

echo "Esperando arranque del motor SQL Server..."
sleep 25s

# Inyección del script SQL adaptado al DER
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "AgroVisionSecure2026!" -d master -i /usr/config/script.sql -C

echo "¡Estructura de base de datos relacional creada!"

using CoffeePestDetection.Application.Commons;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CoffeePestDetection.Application.Exceptions;

public static class ExceptionHelper
{
    public static async Task WriteResponse(HttpContext context, int statusCode, string message)
    {
        context.Response.StatusCode = statusCode;

        context.Response.ContentType = "application/json";

        // 1. Determinamos un mensaje de error detallado según el código de estado
        string tipoError = statusCode switch
        {
            StatusCodes.Status400BadRequest => "La solicitud es incorrecta o contiene datos inválidos.",
            StatusCodes.Status401Unauthorized => "No está autorizado para acceder a este recurso.",
            StatusCodes.Status403Forbidden => "No tiene permisos suficientes para realizar esta acción.",
            StatusCodes.Status404NotFound => "El recurso solicitado no fue encontrado en el sistema.",
            StatusCodes.Status500InternalServerError => "Ocurrió un error inesperado en el servidor.",
            _ => "Ocurrió un error inesperado al procesar la solicitud." // Caso por defecto
        };


        var apiResponse = ApiResponse<object>.Fail(message, [tipoError]);

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(apiResponse, options));
    }
}

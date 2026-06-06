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

        var apiResponse = ApiResponse<object>.Fail(message);

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(apiResponse, options));
    }
}

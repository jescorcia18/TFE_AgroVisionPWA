using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace CoffeePestDetection.Application.Exceptions;

public static class ExceptionHelper
{
    public static async Task WriteResponse(HttpContext context, int statusCode, string message)
    {
        context.Response.StatusCode = statusCode;

        context.Response.ContentType = "application/json";

        var response = new
        {
            success = false,
            message
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}

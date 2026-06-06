using CoffeePestDetection.Application.Exceptions;

namespace CoffeePestDetection.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (UnauthorizedException ex)
        {
            await ExceptionHelper.WriteResponse(
                context,
                StatusCodes.Status401Unauthorized,
                ex.Message);
        }
        catch (NotFoundException ex)
        {
            await ExceptionHelper.WriteResponse(
                context,
                StatusCodes.Status404NotFound,
                ex.Message);
        }
        catch (BadRequestException ex)
        {
            await ExceptionHelper.WriteResponse(
                context,
                StatusCodes.Status400BadRequest,
                ex.Message);
        }
        catch (Exception ex)
        {
            await ExceptionHelper.WriteResponse(
                context,
                StatusCodes.Status500InternalServerError,
                ex.Message);
        }
    }
}

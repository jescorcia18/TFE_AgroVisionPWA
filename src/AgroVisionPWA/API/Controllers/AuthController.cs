using CoffeePestDetection.Application.Commons;
using CoffeePestDetection.Application.Exceptions;
using CoffeePestDetection.Application.Features.Auth.DTOs;
using CoffeePestDetection.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoffeePestDetection.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        var response = await _authService.LoginAsync(request);
        return Ok(ApiResponse<LoginResponseDto>
            .Ok(
                response,
                "Autenticación exitosa"));
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterRequestDto request)
    {
        var result = await _authService.RegisterAsync(request);

        return Ok(
            ApiResponse<RegisterResponseDto>.Ok(result, "Usuario registrado correctamente."));
    }

    //Protegido para pruebas
    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        return Ok(new
        {
            Message = "Token válido"
        });
    }
}

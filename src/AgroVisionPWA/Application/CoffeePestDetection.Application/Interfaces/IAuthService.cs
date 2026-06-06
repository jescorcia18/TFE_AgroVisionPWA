using CoffeePestDetection.Application.Features.Auth.DTOs;

namespace CoffeePestDetection.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
        Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request);
    }
}

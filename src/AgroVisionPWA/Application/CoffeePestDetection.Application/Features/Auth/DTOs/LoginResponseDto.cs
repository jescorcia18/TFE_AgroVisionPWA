namespace CoffeePestDetection.Application.Features.Auth.DTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = null!;
        public DateTime Expiration { get; set; }
        public string FullName { get; set; } = string.Empty;
    }
}

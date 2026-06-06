using CoffeePestDetection.Application.Exceptions;
using CoffeePestDetection.Application.Features.Auth.DTOs;
using CoffeePestDetection.Application.Interfaces;
using CoffeePestDetection.Infrastructure.Persistence;
using CoffeePestDetection.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtService _jwtService;

        public AuthService(ApplicationDbContext context, JwtService jwtService)
        {
            _context = context;        
            _jwtService = jwtService;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await _context.Profiles
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user == null)
                throw new UnauthorizedException("Usuario o contraseña inválidos");

            var valid = PasswordHasher.Verify(request.Password, user.PasswordHash);

            if (!valid)
                throw new UnauthorizedException("Usuario o contraseña inválidos");

            var token = _jwtService.GenerateToken(user);

            return new LoginResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(8),
                FullName = user.FullName
            };
        }
    }
}

using CoffeePestDetection.Application.Commons;
using CoffeePestDetection.Application.Exceptions;
using CoffeePestDetection.Application.Features.Auth.DTOs;
using CoffeePestDetection.Application.Interfaces;
using CoffeePestDetection.Domain.Entities;
using CoffeePestDetection.Domain.Enums;
using CoffeePestDetection.Domain.Enums.Features.Auth;
using CoffeePestDetection.Infrastructure.Persistence;
using CoffeePestDetection.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;

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

        public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            if (await _context.Profiles.AnyAsync(x => x.Email == request.Email))
            {
                throw new BadRequestException("El correo ya existe.");
            }

            //  LÓGICA DE INTEGRACIÓN Y VALIDACIÓN DEL ENUM PARA ROLE
            AuthEnum.Roles rolFinal;

            if (string.IsNullOrWhiteSpace(request.Role))
            {
                // Si el frontend no envía un rol, asignamos el valor por defecto directamente como Enum
                rolFinal = AuthEnum.Roles.Farmer;
            }
            else
            {
                // Si envía un rol, usamos método dinámico para transformarlo y validarlo
                var rolValidado = EnumExtensions.GetFromDescription<AuthEnum.Roles>(request.Role);

                // Si no se encuentra el rol en el sistema, disparamos el error de negocio
                if (rolValidado == null)
                {
                    throw new BadRequestException($"El rol '{request.Role}' proporcionado no es válido.");
                }

                // PROTECCIÓN: Si el endpoint es público, bloquea la autocración de Administradores
                if (rolValidado.Value == AuthEnum.Roles.Admin)
                {
                    throw new BadRequestException("No tienes permisos para registrar un usuario.");
                }

                rolFinal = rolValidado.Value;
            }

            // VALIDAMOS SI LA ORGANIZACION EXISTE
            if (request.OrganizationId.HasValue)
            {
                var exists = await _context.Organizations.AnyAsync(x => x.Id == request.OrganizationId);

                if (!exists)
                    throw new NotFoundException("La organización no existe.");
            }

            var role = AuthEnum.ParseRole(request.Role);

            var profile = new Profile
            {
                Id = Guid.NewGuid(),
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = PasswordHasher.Hash(request.Password),
                Role = role,
                OrganizationId = request.OrganizationId ?? Guid.Empty
            };

            _context.Profiles.Add(profile);

            await _context.SaveChangesAsync();

            return new RegisterResponseDto
            {
                Id = profile.Id,
                FullName = profile.FullName,
                Email = profile.Email,
                Role = profile.Role.GetDescription()
            };
        }
    }
}

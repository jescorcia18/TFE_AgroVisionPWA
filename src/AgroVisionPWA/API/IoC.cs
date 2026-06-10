using CoffeePestDetection.Application.Interfaces;
using CoffeePestDetection.Infrastructure.Persistence;
using CoffeePestDetection.Infrastructure.Security;
using CoffeePestDetection.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace CoffeePestDetection.API
{
    public static class IoC
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. Configuración de DbContext
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions => sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null
                    )
                )
            );

            // 2. Servicios Scoped (Se crea uno por cada solicitud HTTP)
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IOrganizationService, OrganizationService>();

            // 3. Servicios Singleton (Una sola instancia para toda la vida de la app)
            // Usamos el valor desde el appsettings.json en lugar de escribir la clave aquí
            //var jwtKey = configuration["JwtSettings:SecretKey"] ?? "SUPER_SECRET_KEY_123";
            //services.AddSingleton(new JwtService(jwtKey));
            services.Configure<JwtOptions>(configuration.GetSection("JwtSettings"));
            services.AddScoped<JwtService>();

            return services;
        }
    }
}

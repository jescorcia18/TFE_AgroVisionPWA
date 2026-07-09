using CoffeePestDetection.API;
using CoffeePestDetection.API.Middlewares;
using CoffeePestDetection.Infrastructure.Persistence;
using CoffeePestDetection.Infrastructure.Security;
using CoffeePestDetection.Infrastructure.Security.Seeders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Llamar al metodo de extensión.
builder.Services.AddAppServices(builder.Configuration);

//Configurar JWT Middleware
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtSettings"));

//Configurar la authentication
builder.Services
.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    var jwtSection =
        builder.Configuration.GetSection("JwtSettings");

    var key = jwtSection["SecretKey"];

    options.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSection["Issuer"],

            ValidAudience = jwtSection["Audience"],

            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(key!))
        };
});

// 1. Definir la política de CORS produccion
/*builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // El puerto de tu React/Vite
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Necesario si usas JWT en HttpOnly Cookies o SignalR
    });
});*/

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodoConCredenciales", policy =>
    {
        policy.SetIsOriginAllowed(origin => true) // <--- Evalúa cualquier origen como válido
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Ahora sí te deja usarlo sin romper la app
    });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "AgroVision API", Version = "v1" });

    // Aquí pegas tu configuración de seguridad:
    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header
        });

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference =
                        new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                },
                Array.Empty<string>()
            }
        });
});

var app = builder.Build();

//se agrega el Middleware global
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.MapOpenApi();
//}

// Agregar ejecución de Seed para usuario Admin
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var context =
            scope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>();

        await DataSeeder.SeedAsync(context);
    }
}

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseStaticFiles(); //Habilitar archivos estáticos (para el IAModel)

app.UseCors("PermitirTodoConCredenciales"); // Cors

app.UseAuthentication(); //pipeline

app.UseAuthorization();

app.MapControllers();

app.Run();

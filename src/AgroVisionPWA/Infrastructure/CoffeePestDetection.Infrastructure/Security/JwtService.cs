using CoffeePestDetection.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoffeePestDetection.Infrastructure.Security
{
    public class JwtService
    {
        //private readonly string _key;

        //public JwtService(string key)
        //{
        //    _key = key;
        //}

        private readonly JwtOptions _options;
        public JwtService(
            IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public string GenerateToken(Profile profile)
        {
            //var claims = new[]
            //{
            //    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            //    new Claim(ClaimTypes.Email, email)
            //};

            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //var token = new JwtSecurityToken(
            //    expires: DateTime.UtcNow.AddHours(8),
            //    claims: claims,
            //    signingCredentials: creds
            //);

            //return new JwtSecurityTokenHandler().WriteToken(token);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, profile.Id.ToString()),
                new(ClaimTypes.Email, profile.Email),
                new("organizationId", profile.OrganizationId.ToString()),
                new(ClaimTypes.Role, profile.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.SecretKey));

            var credentials =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_options.ExpirationHours),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}

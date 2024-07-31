using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MotorcycleRentalSystem.Domain.Configurations;
using MotorcycleRentalSystem.Domain.Entities.User;
using MotorcycleRentalSystem.Domain.Interfaces.Services.Jwt;
using MotorcycleRentalSystem.Domain.Reponses.Token;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MotorcycleRentalSystem.Application.Services.Jwt
{
    public class JwtProviderService : IJwtProviderService
    {
        private readonly JwtConfiguration _jwtConfiguration;

        public JwtProviderService(IOptions<JwtConfiguration> options)
        {
            _jwtConfiguration = options.Value;
        }

        public TokenResponse Generate(UserEntity user)
        {
            var claims = new Claim[] 
            { 
                new Claim("Email", user.Email),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Role, user.Type.ToString())
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtConfiguration.Secret)), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _jwtConfiguration.EmitedBy,
                _jwtConfiguration.ValidatedIn,
                claims,
                null,
                DateTime.UtcNow.AddHours(_jwtConfiguration.ExpirationInHour),
                signingCredentials
                );

            var tokenValue = new TokenResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpireInHour = _jwtConfiguration.ExpirationInHour
            };

            return tokenValue;
        }
    }
}

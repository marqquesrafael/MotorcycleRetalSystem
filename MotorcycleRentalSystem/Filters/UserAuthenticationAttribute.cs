using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MotorcycleRentalSystem.Domain.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;

namespace MotorcycleRentalSystem.WebApi.Filters
{
    public class UserAuthenticationAttribute : ActionFilterAttribute
    {
        private readonly JwtConfiguration _jwtConfiguration;

        public UserAuthenticationAttribute(IOptions<JwtConfiguration> option)
        {
            _jwtConfiguration = option.Value;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Secret))
                };

                string? token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault().Split(" ").LastOrDefault();

                if (string.IsNullOrEmpty(token))
                {
                    context.Result = new ContentResult
                    {
                        StatusCode = (int)HttpStatusCode.Unauthorized,
                        Content = "Token não encontrado."
                    };

                    return;
                }

                var jwtTokenHandler = new JwtSecurityTokenHandler();

                var tokenVerification = jwtTokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

                if (validatedToken is JwtSecurityToken securityToken)
                {
                    var result = securityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                    if (result is false)
                    {
                        context.Result = new ContentResult
                        {
                            StatusCode = (int)HttpStatusCode.Unauthorized,
                            Content = "Token inválido."
                        };

                        return;
                    }
                }

                var userEmail = tokenVerification.Claims.Where(p => p.Type == "Email").FirstOrDefault();

                context.HttpContext.Items.Add("userEmail", userEmail.Value);
            }
            catch (Exception)
            {
                context.Result = new ContentResult
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Content = "Token inválido."
                };

                return;
            }

            base.OnActionExecuting(context);
        }

    }
}

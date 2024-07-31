using MotorcycleRentalSystem.Domain.Exceptions;
using MotorcycleRentalSystem.Domain.Interfaces.Services.Jwt;
using MotorcycleRentalSystem.Domain.Interfaces.Services.Login;
using MotorcycleRentalSystem.Domain.Interfaces.Services.User;
using MotorcycleRentalSystem.Domain.Reponses.Token;

namespace MotorcycleRentalSystem.Application.Services.Login
{
    public class LoginService : ILoginService
    {
        private readonly IJwtProviderService _jwtProviderService;
        private readonly IUserService _userService;

        public LoginService(IJwtProviderService jwtProviderService, IUserService userService)
        {
            _jwtProviderService = jwtProviderService;
            _userService = userService;
        }

        public TokenResponse Authenticate(string email, string password)
        {
            var user = _userService.FindUserByEmailAndPassword(email, password);

            if (user is null)
                throw new InvalidCredentialsException();

            TokenResponse token = _jwtProviderService.Generate(user);

            return token;
        }
    }
}

using MotorcycleRentalSystem.Domain.Entities.User;
using MotorcycleRentalSystem.Domain.Interfaces.Repositories.User;
using MotorcycleRentalSystem.Domain.Interfaces.Services.User;

namespace MotorcycleRentalSystem.Application.Services.User
{
    public class UserService : BaseService<UserEntity>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRrepository) : base(userRrepository)
        {
            _userRepository = userRrepository;
        }

        public UserEntity FindUserByEmail(string email)
        {
            var user = _userRepository.Select()
                .Where(p => p.Email == email && p.Active)
                .FirstOrDefault();

            return user;
        }

        public UserEntity FindUserByEmailAndPassword(string email, string password)
        {
            return _userRepository.GetUserByEmailAndPassword(email, password);
        }
    }
}

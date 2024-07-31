using MotorcycleRentalSystem.Domain.Entities.User;
using MotorcycleRentalSystem.Domain.Interfaces.Repositories.User;
using MotorcycleRentalSystem.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleRentalSystem.Infrastructure.Persistence.Repositories.User
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(MotorcycleRentalSystemDbContext dbContext) : base(dbContext) { }

        public UserEntity GetUserByEmailAndPassword(string email, string password)
        {
            var user = Select()
                .Where(p =>
                       p.Email == email &&
                       p.Password == password &&
                       p.Active)
                .FirstOrDefault();

            return user;
        }

    }
}

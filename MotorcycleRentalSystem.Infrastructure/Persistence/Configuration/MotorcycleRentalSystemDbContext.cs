using Microsoft.EntityFrameworkCore;
using MotorcycleRentalSystem.Infrastructure.Mapping.User;
using MotorcycleRentalSystem.Infrastructure.Persistence.Mapping.Motorcycle;
using MotorcycleRentalSystem.Infrastructure.Persistence.Mapping.Rider;

namespace MotorcycleRentalSystem.Infrastructure.Configuration
{
    public class MotorcycleRentalSystemDbContext : DbContext
    {
        public MotorcycleRentalSystemDbContext(DbContextOptions<MotorcycleRentalSystemDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new MotorcycleMap());
            modelBuilder.ApplyConfiguration(new CurrentYearMotorcycleMap());
            modelBuilder.ApplyConfiguration(new RiderMap());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}

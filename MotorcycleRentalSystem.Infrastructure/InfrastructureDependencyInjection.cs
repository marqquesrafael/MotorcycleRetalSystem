using Hangfire.Storage.Monitoring;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MotorcycleRentalSystem.Domain.Configurations;
using MotorcycleRentalSystem.Domain.Interfaces.Repositories.Motorcycle;
using MotorcycleRentalSystem.Domain.Interfaces.Repositories.Rider;
using MotorcycleRentalSystem.Domain.Interfaces.Repositories.User;
using MotorcycleRentalSystem.Domain.Interfaces.Services.RabbitMq;
using MotorcycleRentalSystem.Domain.Interfaces.Services.Storage;
using MotorcycleRentalSystem.Infrastructure.Configuration;
using MotorcycleRentalSystem.Infrastructure.Persistence.Repositories.Motorcyle;
using MotorcycleRentalSystem.Infrastructure.Persistence.Repositories.Rider;
using MotorcycleRentalSystem.Infrastructure.Persistence.Repositories.User;
using MotorcycleRentalSystem.Infrastructure.RabbitMq;
using MotorcycleRentalSystem.Infrastructure.Storage;
using MotorcycleRentalSystem.WebApi.RabbitConsumer;

namespace MotorcycleRentalSystem.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddPersistenceConfiguration(this IServiceCollection services, string connectionString)
        {

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            services.AddDbContext<MotorcycleRentalSystemDbContext>((sp, options) =>
            {
                options.UseApplicationServiceProvider(sp);
                options.UseLazyLoadingProxies();
                options.UseNpgsql(connectionString, b =>
                {
                    b.MigrationsAssembly("MotorcycleRentalSystem.Infrastructure");
                });
            }, ServiceLifetime.Scoped);

            return services;
        }

        public static IServiceCollection AddRepositoriesResolvers(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
            services.AddScoped<ICurrentYearMotorcycleRepository, CurrentYearMotorcycleRepository>();
            services.AddScoped<IRiderRepository, RiderRepository>();

            return services;
        }

        public static IServiceCollection AddRabbitMqResolver(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("Rabbit");
            services.Configure<RabbitConfiguration>(section);

            services.AddScoped<IRabbitMqClient, RabbitMqClient>();
            services.AddHostedService<RabbitMqConsumer>();

            return services;
        }

        public static IServiceCollection AddStorageResolver(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
            return services;
        }

    }
}
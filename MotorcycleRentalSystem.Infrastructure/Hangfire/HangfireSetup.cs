using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.DependencyInjection;

namespace MotorcycleRentalSystem.Infrastructure.Hangfire
{
    public static class HangfireSetup
    {

        public static IServiceCollection AddHangfire(this IServiceCollection services)
        {
            services.AddHangfire(config =>
            {
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseMemoryStorage();
            });

            services.AddHangfireServer();

            return services;
        }

    }
}

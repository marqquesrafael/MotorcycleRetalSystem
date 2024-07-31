using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MotorcycleRentalSystem.Application.Services.Jwt;
using MotorcycleRentalSystem.Application.Services.Login;
using MotorcycleRentalSystem.Application.Services.Motorcycle;
using MotorcycleRentalSystem.Application.Services.Rider;
using MotorcycleRentalSystem.Application.Services.User;
using MotorcycleRentalSystem.Application.Validators;
using MotorcycleRentalSystem.Domain.Configurations;
using MotorcycleRentalSystem.Domain.Entities.Motorcycle;
using MotorcycleRentalSystem.Domain.Entities.Rider;
using MotorcycleRentalSystem.Domain.Interfaces.Services.Jwt;
using MotorcycleRentalSystem.Domain.Interfaces.Services.Login;
using MotorcycleRentalSystem.Domain.Interfaces.Services.Motorcycle;
using MotorcycleRentalSystem.Domain.Interfaces.Services.Rider;
using MotorcycleRentalSystem.Domain.Interfaces.Services.User;
using MotorcycleRentalSystem.Domain.Reponses.Motorcycle;
using MotorcycleRentalSystem.Domain.Requests;
using System.Text;

namespace MotorcycleRentalSystem.Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddServicesResolvers(this IServiceCollection services)
        {
            services.AddScoped<IJwtProviderService, JwtProviderService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMotorcycleService, MotorcycleService>();
            services.AddScoped<ICurrentYearMotorcycleService, CurrentYearMotorcycleService>();
            services.AddScoped<IRiderService, RiderService>();

            return services;
        }

        public static IServiceCollection AddServicesMapping(this IServiceCollection services)
        {
            services.AddSingleton(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddMotorcycleRequest, MotorcycleEntity>();
                cfg.CreateMap<MotorcycleEntity, MotorcycleResponse>();
                cfg.CreateMap<RegisterRiderRequest, RiderEntity>();
            }).CreateMapper());

            return services;
        }

        public static IServiceCollection AddServicesValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<AddMotorcycleRequest>, AddMotorcycleValidator>();
            services.AddScoped<IValidator<UpdateLicensePlateRequest>, UpdateLicensePlateValidator>();
            services.AddScoped<IValidator<RegisterRiderRequest>, RegisterRiderValidator>();

            return services;
        }


        public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfiguration = configuration.GetSection("JWT");
            services.Configure<JwtConfiguration>(jwtConfiguration);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfiguration.GetSection("EmitedBy").Value,
                    ValidAudience = jwtConfiguration.GetSection("ValidatedIn").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.GetSection("Secret").Value))
                });

            return services;
        }
    }
}

using Hangfire;
using Microsoft.OpenApi.Models;
using MotorcycleRentalSystem.Application;
using MotorcycleRentalSystem.Infrastructure;
using MotorcycleRentalSystem.Infrastructure.Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira a palavra \"Bearer\" com um espaço a frente e em seguida o token de autenticação."
    });
    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
          {
            new OpenApiSecurityScheme
            {
               Reference = new OpenApiReference
               {
                   Type = ReferenceType.SecurityScheme,
                   Id = "Bearer"
               }
            },
            new string[] {}
          }
        });
});
builder.Services.AddHangfire();
builder.Services.AddPersistenceConfiguration(builder.Configuration.GetConnectionString("MyServer"));
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddRepositoriesResolvers();
builder.Services.AddRabbitMqResolver(builder.Configuration);
builder.Services.AddServicesResolvers();
builder.Services.AddServicesMapping();
builder.Services.AddServicesValidators();
builder.Services.AddStorageResolver();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard("/hangfire", new DashboardOptions()
{
    DashboardTitle = "Motorcycle Rental System",
    DarkModeEnabled = false,
    DisplayStorageConnectionString = false
});

app.Run();

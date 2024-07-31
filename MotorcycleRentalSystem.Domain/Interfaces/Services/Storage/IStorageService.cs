using Microsoft.AspNetCore.Http;

namespace MotorcycleRentalSystem.Domain.Interfaces.Services.Storage
{
    public interface IStorageService
    {
        Task<string> SaveImageAsync(IFormFile imageFile);
    }
}

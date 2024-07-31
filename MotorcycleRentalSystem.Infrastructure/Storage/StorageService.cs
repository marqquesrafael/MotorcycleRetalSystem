using Microsoft.AspNetCore.Http;
using MotorcycleRentalSystem.Domain.Exceptions;
using MotorcycleRentalSystem.Domain.Interfaces.Services.Storage;

namespace MotorcycleRentalSystem.Infrastructure.Storage
{
    public class StorageService : IStorageService
    {
        private readonly string _targetFolder;

        public StorageService()
        {
            var path = Directory.GetCurrentDirectory();
            _targetFolder = Path.Combine(path, "driver_licenses_Images");

            if (!Directory.Exists(_targetFolder))
                Directory.CreateDirectory(_targetFolder);
        }

        public async Task<string> SaveImageAsync(IFormFile imageFile)
        {           
            var fileExtension = Path.GetExtension(imageFile.FileName).ToLower();

            if (fileExtension != ".png" && fileExtension != ".bmp")
                throw new InvalidImageTypeException();
            
            var filePath = Path.Combine(_targetFolder, imageFile.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return filePath;
        }
    }
}

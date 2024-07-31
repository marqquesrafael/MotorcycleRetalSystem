using AutoMapper;
using FluentValidation;
using MotorcycleRentalSystem.Application.Validators;
using MotorcycleRentalSystem.Domain.Entities.Rider;
using MotorcycleRentalSystem.Domain.Exceptions;
using MotorcycleRentalSystem.Domain.Interfaces.Repositories;
using MotorcycleRentalSystem.Domain.Interfaces.Repositories.Rider;
using MotorcycleRentalSystem.Domain.Interfaces.Services.Rider;
using MotorcycleRentalSystem.Domain.Interfaces.Services.Storage;
using MotorcycleRentalSystem.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleRentalSystem.Application.Services.Rider
{
    public class RiderService : BaseService<RiderEntity>, IRiderService
    {
        private readonly IRiderRepository _riderRepository;
        private readonly IValidator<RegisterRiderRequest> _validator;
        private readonly IMapper _mapper;
        private readonly IStorageService _storageService;

        public RiderService(
            IRiderRepository riderRepository,
            IValidator<RegisterRiderRequest> validator,
            IMapper mapper,
            IStorageService storageService
            ) : base(riderRepository)
        {
            _riderRepository = riderRepository;
            _validator = validator;
            _mapper = mapper;
            _storageService = storageService;
        }

        public async Task Register(RegisterRiderRequest request)
        {
            try
            {
                var validate = await _validator.ValidateAsync(request);

                if (validate.Errors.Any())
                    throw new ValidatorException(string.Join("\n", validate.Errors));

                var entity = _mapper.Map<RiderEntity>(request);

                var imageKey = await _storageService.SaveImageAsync(request.DriverLicenseImage);
                entity.DriverLicenseImageKey = imageKey;

                Create(entity);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}

using AutoMapper;
using FluentValidation;
using MotorcycleRentalSystem.Domain.Entities.Motorcycle;
using MotorcycleRentalSystem.Domain.Exceptions;
using MotorcycleRentalSystem.Domain.Interfaces.Repositories.Motorcycle;
using MotorcycleRentalSystem.Domain.Interfaces.Services.Motorcycle;
using MotorcycleRentalSystem.Domain.Interfaces.Services.RabbitMq;
using MotorcycleRentalSystem.Domain.Interfaces.Services.User;
using MotorcycleRentalSystem.Domain.Reponses.Motorcycle;
using MotorcycleRentalSystem.Domain.Requests;

namespace MotorcycleRentalSystem.Application.Services.Motorcycle
{
    public class MotorcycleService : BaseService<MotorcycleEntity>, IMotorcycleService
    {
        private readonly IUserService _userService;
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IValidator<AddMotorcycleRequest> _validatorAddMotorcycle;
        private readonly IMapper _mapper;
        private readonly IRabbitMqClient _rabbitMqClient;
        private readonly IValidator<UpdateLicensePlateRequest> _validatorUpdateLicensePlate;

        public MotorcycleService(
            IMotorcycleRepository motorcycleRepository,
            IUserService userService,
            IValidator<AddMotorcycleRequest> validatorAddMotorcycle,
            IMapper mapper,
            IRabbitMqClient rabbitMqClient,
            IValidator<UpdateLicensePlateRequest> validatorUpdateLicensePlate) : base(motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
            _userService = userService;
            _validatorAddMotorcycle = validatorAddMotorcycle;
            _mapper = mapper;
            _rabbitMqClient = rabbitMqClient;
            _validatorUpdateLicensePlate = validatorUpdateLicensePlate;
        }

        public IEnumerable<TResponse> GetAll<TResponse>()
        {
            var motorcycles = _motorcycleRepository.Select();

            foreach (var item in motorcycles)
                yield return _mapper.Map<TResponse>(item);
        }

        public MotorcycleResponse GetByLicensePlate(string licensePlate)
        {
            var entity = _motorcycleRepository.Select()
                .Where(p => p.Active && p.LicensePlate == licensePlate)
                .FirstOrDefault();

            return _mapper.Map<MotorcycleResponse>(entity);
        }

        public async Task Register(AddMotorcycleRequest request, string userEmail)
        {
            var validate = await _validatorAddMotorcycle.ValidateAsync(request);

            if (validate.Errors.Any())
                throw new ValidatorException(string.Join("\n", validate.Errors));

            var user = _userService.FindUserByEmail(userEmail) ?? throw new UserNotFoundException(userEmail);

            if (!user.IsAdministrator())
                throw new UserWithoutPermissionException();

            bool motorcycleAlreadyExists = _motorcycleRepository.HasMotorcycleByLicensePlate(request.LicensePlate);

            if (motorcycleAlreadyExists)
                throw new MotorcycleAlreadyExistsException(request.LicensePlate);

            var entity = _mapper.Map<MotorcycleEntity>(request);
            _motorcycleRepository.Insert(entity);

            var messaRequest = new MotorcycleRegisteredMessageEvent(entity.Id, entity.Year, entity.Model, entity.LicensePlate, user.Id);
            _rabbitMqClient.Publish(messaRequest);

        }

        public async Task UpdateLicensePlate(UpdateLicensePlateRequest request)
        {
            var validate = await _validatorUpdateLicensePlate.ValidateAsync(request);

            if (validate.Errors.Any())
                throw new ValidatorException(string.Join("\n", validate.Errors));

            var motorcycle = _motorcycleRepository.GetById(request.Id) ?? throw new MotorcycleNotFoundException(request.Id);

            motorcycle.LicensePlate = request.NewLicensePlate;
            _motorcycleRepository.Update(motorcycle);

        }
    }
}

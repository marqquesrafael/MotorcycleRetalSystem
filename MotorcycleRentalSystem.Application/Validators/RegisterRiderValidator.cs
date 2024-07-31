using FluentValidation;
using MotorcycleRentalSystem.Domain.Enums;
using MotorcycleRentalSystem.Domain.Interfaces.Repositories.Rider;
using MotorcycleRentalSystem.Domain.Requests;
using System.Text.RegularExpressions;

namespace MotorcycleRentalSystem.Application.Validators
{
    public class RegisterRiderValidator : AbstractValidator<RegisterRiderRequest>
    {
        private readonly IRiderRepository _repository;

        public RegisterRiderValidator(IRiderRepository repository)
        {
            _repository = repository;

            RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Nome é obrigatório.");

            RuleFor(p => p.Cnpj)
                .NotEmpty().WithMessage("CNPJ é obrigatório.");

            RuleFor(p => p.Cnpj)
                .Must(cnpj => Regex.IsMatch(cnpj, @"^\d+$")).WithMessage("CNPJ Inválido.")
                .Must(cnpj => !_repository.IsCnpjAlreadyRegistered(cnpj)).WithMessage("O CNPJ já está cadastrado.")
                .When(p => p.Cnpj is not null);


            RuleFor(p => p.BirthDate)
                .NotEmpty().WithMessage("Data de nascimento é obrigatória.")
                .LessThan(DateTime.Now).WithMessage("Data de nascimento inválida.");

            RuleFor(p => p.DriverLicenseNumber)
                .NotEmpty().WithMessage("Número da CNH é obrigatório.");

            RuleFor(p => p.DriverLicenseNumber)
                .Must(cnh => Regex.IsMatch(cnh, @"^\d+$")).WithMessage("Número de CNH Inválido.")
                .Must(cnh => !_repository.IsDriverLicenseAlreadyRegistered(cnh)).WithMessage("O número da CNH já está cadastrado.")
                .When(p => p.DriverLicenseNumber is not null);


            RuleFor(p => p.DriverLicenseType)
                .NotEmpty().WithMessage("Tipo de CNH é obrigatório");

            RuleFor(p => p.DriverLicenseType)
                .Must(p => Enum.IsDefined(typeof(DriverLicenseTypeEnum), p))
                .When(p => p.DriverLicenseType is not null)
                .WithMessage("Tipo de CNH inválido. Os tipos válidos são A, B ou AB.");

            RuleFor(p => p.DriverLicenseImage)
                .NotNull().WithMessage("Imagem da CNH é obrigatória.");

            RuleFor(p => p.DriverLicenseImage)
               .Must(file => file?.Length > 0)
               .When(p => p.DriverLicenseImage is not null).WithMessage("Imagem da CNH não pode estar vazia.");
            

            RuleFor(p => p.DriverLicenseImage)
               .Must(file => Path.GetExtension(file?.FileName).ToLower() != ".png" && Path.GetExtension(file?.FileName).ToLower() != ".bmp")
               .When(p => p.DriverLicenseImage is not null)
               .When(p => p.DriverLicenseImage?.Length > 0)
               .WithMessage("Formato do arquivo de imagem da CNH inválido. Apenas PNG e BMP são suportados.");
        }
    }
}

using FluentValidation;
using MotorcycleRentalSystem.Domain.Requests;

namespace MotorcycleRentalSystem.Application.Validators
{
    public class AddMotorcycleValidator : AbstractValidator<AddMotorcycleRequest>
    {
        public AddMotorcycleValidator()
        {
            RuleFor(p => p.Model)
                .NotEmpty()
                .WithMessage("Favor informar o modelo");

            RuleFor(p => p.Year)
                .NotEmpty()
                .WithMessage("Favor informar o ano");

            RuleFor(p => p.LicensePlate)
                .NotEmpty()
                .WithMessage("Favor informar a placa");
        }
    }
}

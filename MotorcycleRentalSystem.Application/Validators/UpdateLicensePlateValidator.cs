using FluentValidation;
using MotorcycleRentalSystem.Domain.Requests;

namespace MotorcycleRentalSystem.Application.Validators
{
    public class UpdateLicensePlateValidator : AbstractValidator<UpdateLicensePlateRequest>
    {
        public UpdateLicensePlateValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                .WithMessage("Favor informar um identificador válido");

            RuleFor(p => p.NewLicensePlate)
                .NotEmpty()
                .WithMessage("Favor informar uma placa válida");
        }
    }
}

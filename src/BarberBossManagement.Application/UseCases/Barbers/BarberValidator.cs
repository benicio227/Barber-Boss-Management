using BarberBossManagement.Communication.Requests;
using BarberBossManagement.Exception;
using FluentValidation;

namespace BarberBossManagement.Application.UseCases.Barbers;
public class BarberValidator : AbstractValidator<RequestBarberShopJson>
{
    public BarberValidator()
    {
        RuleFor(barber => barber.Name).NotEmpty().WithMessage(ResourceErrorMessages.THE_NAME_IS_REQUIRED);
        RuleFor(barber => barber.Address).NotEmpty().WithMessage(ResourceErrorMessages.THE_ADDRESS_IS_REQUIRED);
        RuleFor(barber => barber.PhoneNumber)
            .NotEmpty().WithMessage(ResourceErrorMessages.THE_PHONE_NUMBER_IS_REQUIRED)
            .Matches(@"^\+?\d{10,15}$").WithMessage(ResourceErrorMessages.INVALID_PHONE_NUMBER);
    }
}

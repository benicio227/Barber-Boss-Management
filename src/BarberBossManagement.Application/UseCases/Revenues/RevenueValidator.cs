using BarberBossManagement.Communication.Requests;
using BarberBossManagement.Exception;
using FluentValidation;

namespace BarberBossManagement.Application.UseCases.Revenues;
public class RevenueValidator : AbstractValidator<RequestRevenueJson>
    // RevenueValidator é uma classe publica que representa um validador para objetos do tipo RequestRevenueJson
{
    public RevenueValidator()
    {
        RuleFor(revenue => revenue.Title).NotEmpty().WithMessage(ResourceErrorMessages.THE_TITLE_IS__REQUIRED);
        RuleFor(revenue => revenue.Amount).GreaterThan(0).WithMessage(ResourceErrorMessages.THE__AMOUNT_MUST_BE_GREATER_THAN_ZERO);
        RuleFor(revenue => revenue.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMessages.REVENUES_CANNOT_BE_FOR_THE_FUTURE);
        RuleFor(revenue => revenue.PaymentType).IsInEnum().WithMessage(ResourceErrorMessages.PAYMENT_TYPE_IS_NOT_VALID);
    }
}

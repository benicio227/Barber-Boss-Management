using BarberBossManagement.Application.UseCases.Revenues;
using BarberBossManagement.Communication.Enums;
using BarberBossManagement.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Revenues.Register;
public class RegisterRevenueValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new RevenueValidator();
        var request = RequestRegisterRevenueJsonBuilder.Build();
        

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData(null)]
    public void Error_Title_Empty(string title)
    {
        var validator = new RevenueValidator();
        var request = RequestRegisterRevenueJsonBuilder.Build();
        request.Title = title;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.THE_TITLE_IS__REQUIRED));
    }

    [Fact]
    public void Error_Date_Future()
    {
        var validator = new RevenueValidator();
        var request = RequestRegisterRevenueJsonBuilder.Build();
        request.Date = DateTime.UtcNow.AddDays(1);

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.REVENUES_CANNOT_BE_FOR_THE_FUTURE));
    }

    [Fact]
    public void Error_PaymentType_Invalid()
    {
        var validator = new RevenueValidator();
        var request = RequestRegisterRevenueJsonBuilder.Build();
        request.PaymentType = (PaymentType)700;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PAYMENT_TYPE_IS_NOT_VALID));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-2)]
    [InlineData(-7)]
    public void Error_Amount_Invalid(decimal amount)
    {
        var validator = new RevenueValidator();
        var request = RequestRegisterRevenueJsonBuilder.Build();
        request.Amount = amount;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.THE__AMOUNT_MUST_BE_GREATER_THAN_ZERO));
    }
}

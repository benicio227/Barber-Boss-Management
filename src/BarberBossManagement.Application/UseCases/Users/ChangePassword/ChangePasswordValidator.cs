﻿using BarberBossManagement.Communication.Requests;
using FluentValidation;

namespace BarberBossManagement.Application.UseCases.Users.ChangePassword;
public class ChangePasswordValidator : AbstractValidator<RequestChangePasswordJson>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.NewPassword).SetValidator(new PasswordValidator<RequestChangePasswordJson>());
    }
}

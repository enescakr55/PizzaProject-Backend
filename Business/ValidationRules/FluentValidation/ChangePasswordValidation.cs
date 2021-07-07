using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class ChangePasswordValidation:AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordValidation()
        {
            RuleFor(p => p.NewPassword).NotEmpty();
            RuleFor(p => p.OldPassword).NotEmpty();
            RuleFor(p => p.NewPassword).MinimumLength(8).WithMessage("Şifreniz minimum 8 karakter olmalıdır");
        }
    }
}

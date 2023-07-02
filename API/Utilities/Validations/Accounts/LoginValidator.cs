using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts;

public class LoginValidator : AbstractValidator<LoginAccountDto>
{
    public LoginValidator()
    {
        RuleFor(p => p.Email)
           .NotEmpty()
           .EmailAddress();
        
        RuleFor(p => p.Password)
           .NotEmpty();
    }
}

﻿using API.DTOs.Roles;
using FluentValidation;

namespace API.Utilities.Validations.Roles;

public class RoleValidator : AbstractValidator<GetRoleDto>
{
    public RoleValidator()
    {
        RuleFor(p => p.Guid)
           .NotEmpty();
        
        RuleFor(p => p.Name)
           .NotEmpty();
    }
}

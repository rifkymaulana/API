using API.DTOs.Rooms;
using FluentValidation;

namespace API.Utilities.Validations.Rooms;

public class NewRoomValidator : AbstractValidator<CreateRoomDto>
{
    public NewRoomValidator()
    {
        RuleFor(p => p.Name)
           .NotEmpty();

        RuleFor(p => p.Capacity)
           .NotEmpty();

        RuleFor(p => p.Floor)
           .NotEmpty();
    }
}

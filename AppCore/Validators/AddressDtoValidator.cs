using AppCore.DTOs;
using FluentValidation;

// Piotr Bacior - WSEI Kraków

namespace AppCore.Validators;

public class AddressDtoValidator : AbstractValidator<AddressDto>
{
    public AddressDtoValidator()
    {
        RuleFor(x => x.Street).NotEmpty().MaximumLength(200);
        RuleFor(x => x.City).NotEmpty().MaximumLength(100);
        // Format xx-xxx (np. 12-345)
        RuleFor(x => x.PostalCode).NotEmpty().Matches(@"^\d{2}-\d{3}$").WithMessage("Format musi być xx-xxx.");
        RuleFor(x => x.Country).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Type).IsInEnum();
    }
}
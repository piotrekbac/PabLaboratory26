using AppCore.DTOs;
using FluentValidation;

// Piotr Bacior - WSEI Kraków

namespace AppCore.Validators;

// Walidator DTO adresu — sprawdza poprawność pól
public class AddressDtoValidator : AbstractValidator<AddressDto>
{
    public AddressDtoValidator()
    {
        RuleFor(x => x.Street).NotEmpty().MaximumLength(200);
        RuleFor(x => x.City).NotEmpty().MaximumLength(100);

        // Format kodu pocztowego: 12-345
        RuleFor(x => x.PostalCode)
            .NotEmpty()
            .Matches(@"^\d{2}-\d{3}$")
            .WithMessage("Format musi być xx-xxx.");

        RuleFor(x => x.Country).NotEmpty().MaximumLength(100);

        // Typ adresu musi być poprawnym enumem
        RuleFor(x => x.Type).IsInEnum();
    }
}
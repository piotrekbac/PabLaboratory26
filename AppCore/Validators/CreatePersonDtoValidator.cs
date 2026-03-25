using AppCore.DTOs;
using AppCore.Interfaces;
using FluentValidation;

// Piotr Bacior - WSEI Kraków

namespace AppCore.Validators;

// Walidator DTO tworzenia osoby.
// Sprawdza poprawność imienia, nazwiska, emaila, telefonu itd.
public class CreatePersonDtoValidator : AbstractValidator<CreatePersonDto>
{
    private readonly ICompanyRepository _companyRepository;

    public CreatePersonDtoValidator(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Imię jest wymagane.")
            .MaximumLength(100)
            .Matches(@"^[\p{L}\s\-]+$").WithMessage("Imię zawiera niedozwolone znaki.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Nazwisko jest wymagane.")
            .MaximumLength(200)
            .Matches(@"^[\p{L}\s\-]+$").WithMessage("Nazwisko zawiera niedozwolone znaki.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(200);

        RuleFor(x => x.Phone)
            .Matches(@"^[0-9+\-\s]+$")
            .WithMessage("Nieprawidłowy format numeru telefonu.")
            .When(x => !string.IsNullOrEmpty(x.Phone));

        RuleFor(x => x.BirthDate)
            .LessThan(DateTime.Today.AddYears(-18)).WithMessage("Osoba musi mieć co najmniej 18 lat.")
            .GreaterThan(DateTime.Today.AddYears(-120)).WithMessage("Nieprawidłowa data urodzenia.")
            .When(x => x.BirthDate.HasValue);

        RuleFor(x => x.Gender)
            .IsInEnum()
            .WithMessage("Nieprawidlowa wartość płci.");

        // Walidacja adresu — jeśli istnieje, walidujemy go osobnym walidatorem.
        RuleFor(x => x.Address)
            .SetValidator(new AddressDtoValidator()!)
            .When(x => x.Address is not null);
    }
}
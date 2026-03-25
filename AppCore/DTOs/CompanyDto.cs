using AppCore.Models;

// Piotr Bacior - WSEI Kraków

namespace AppCore.DTOs;

public record CompanyDto
{
    // Unikalny identyfikator firmy
    public Guid Id { get; init; }

    // Nazwa firmy — wymagane pole
    public string Name { get; init; } = string.Empty;

    // Numer NIP — opcjonalny
    public string? NIP { get; init; }

    // Branża, w której działa firma — opcjonalna
    public string? Industry { get; init; }

    // Strona internetowa firmy — opcjonalna
    public string? Website { get; init; }
}

// DTO używane przy tworzeniu nowej firmy.
// Rekord pozycyjny — wszystkie wartości przekazywane w konstruktorze.
public record CreateCompanyDto(string Name, string? NIP, string? Industry, string? Website)
{
    // Konwersja DTO → encja domenowa Company
    public Company ToEntity() => new()
    {
        Id = Guid.NewGuid(),   // Generowanie nowego GUID
        Name = this.Name,
        NIP = this.NIP,
        Industry = this.Industry,
        Website = this.Website
    };
}
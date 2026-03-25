using AppCore.Models.Enums; // Import enumów (np. ContactStatus)

// Piotr Bacior - WSEI Kraków

namespace AppCore.DTOs
{
    // DTO reprezentujący zestaw kryteriów wyszukiwania kontaktów.
    // Używany np. w API do filtrowania listy kontaktów.
    public record ContactSearchDto(
        string? Query,          // Wyszukiwanie tekstowe (imię, nazwisko, firma)
        ContactStatus? Status,  // Filtrowanie po statusie
        string? Tag,            // Filtrowanie po tagu
        string? ContactType,    // Typ kontaktu jako string (Person, Company, Organization)
        int Page = 1,           // Numer strony — domyślnie 1
        int PageSize = 20       // Rozmiar strony — domyślnie 20
    );
}
using AppCore.Models.Enums; // Import enumów (np. ContactStatus)

// Piotr Bacior - WSEI Kraków

namespace AppCore.DTOs
{
    // Rekord reprezentujący zestaw kryteriów wyszukiwania kontaktów.
    // Używany np. w endpointach API do filtrowania listy kontaktów.
    public record ContactSearchDto(
        string? Query,          // Ogólne wyszukiwanie tekstowe (np. imię, nazwisko, nazwa firmy)
        ContactStatus? Status,  // Filtrowanie po statusie kontaktu (np. Active, Inactive)
        string? Tag,            // Filtrowanie po tagu (np. "VIP", "Lead")
        string? ContactType,    // Typ kontaktu (Person, Company, Organization) — jako string
        int Page = 1,           // Numer strony — domyślnie 1
        int PageSize = 20       // Rozmiar strony — domyślnie 20 elementów
    );
}
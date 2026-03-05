using AppCore.Models.Enums; // Import enumów (ContactStatus, AddressType)

namespace AppCore.DTOs
{
    // Bazowy DTO dla wszystkich typów kontaktów (Person, Company, Organization).
    // Zawiera wspólne pola, które każdy kontakt powinien mieć.
    public abstract record ContactBaseDto
    {
        // Unikalny identyfikator kontaktu.
        public Guid Id { get; init; }

        // Email kontaktu — wymagany, domyślnie pusty string.
        public string Email { get; init; } = string.Empty;

        // Numer telefonu — wymagany, domyślnie pusty string.
        public string Phone { get; init; } = string.Empty;

        // Adres kontaktu — opcjonalny (może być null).
        public AddressDto? Address { get; init; }

        // Status kontaktu (np. Active, Inactive, Archived).
        public ContactStatus Status { get; init; }

        // Lista tagów przypisanych do kontaktu (np. "VIP", "Lead").
        // Inicjalizowana pustą listą, aby uniknąć nulli.
        public List<string> Tags { get; init; } = new();

        // Data utworzenia kontaktu.
        public DateTime CreatedAt { get; init; }
    }

    // DTO reprezentujący adres kontaktu.
    // Rekord pozycyjny — wszystkie pola przekazywane w konstruktorze.
    public record AddressDto(
        string Street,      // Ulica i numer
        string City,        // Miasto
        string PostalCode,  // Kod pocztowy
        string Country,     // Kraj
        AddressType Type    // Typ adresu (np. Home, Office)
    );
}
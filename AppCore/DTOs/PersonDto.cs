using AppCore.Models.Enums; // Import enumów (Gender, ContactStatus)

namespace AppCore.DTOs
{
    // DTO reprezentujący osobę zwracaną na zewnątrz (np. w API).
    // Dziedziczy po ContactBaseDto, więc ma podstawowe dane kontaktowe.
    public record PersonDto : ContactBaseDto
    {
        // Imię osoby — wymagane, domyślnie pusty string.
        public string FirstName { get; init; } = string.Empty;

        // Nazwisko osoby — wymagane.
        public string LastName { get; init; } = string.Empty;

        // Stanowisko (np. Manager, Developer) — opcjonalne.
        public string? Position { get; init; }

        // Data urodzenia — opcjonalna.
        public DateTime? BirthDate { get; init; }

        // Płeć — enum Gender.
        public Gender Gender { get; init; }

        // Id pracodawcy (Company) — opcjonalne.
        public Guid? EmployerId { get; init; }
    }

    // DTO używane przy tworzeniu nowej osoby.
    // Rekord pozycyjny — wszystkie pola przekazywane w konstruktorze.
    public record CreatePersonDto(
        string FirstName,      // Imię — wymagane
        string LastName,       // Nazwisko — wymagane
        string Email,          // Email — wymagany
        string Phone,          // Telefon — wymagany
        string? Position,      // Stanowisko — opcjonalne
        DateTime? BirthDate,   // Data urodzenia — opcjonalna
        Gender Gender,         // Płeć — wymagane
        Guid? EmployerId,      // Id pracodawcy — opcjonalne
        AddressDto? Address    // Adres — opcjonalny
    );

    // DTO używane przy aktualizacji osoby.
    // Wszystkie pola są opcjonalne — aktualizujesz tylko to, co chcesz zmienić.
    public record UpdatePersonDto(
        string? FirstName,         // Nowe imię lub null (bez zmiany)
        string? LastName,          // Nowe nazwisko lub null
        string? Email,             // Nowy email lub null
        string? Phone,             // Nowy telefon lub null
        string? Position,          // Nowe stanowisko lub null
        DateTime? BirthDate,       // Nowa data urodzenia lub null
        Gender? Gender,            // Nowa płeć lub null
        Guid? EmployerId,          // Nowy pracodawca lub null
        AddressDto? Address,       // Nowy adres lub null
        ContactStatus? Status      // Nowy status kontaktu lub null
    );
}

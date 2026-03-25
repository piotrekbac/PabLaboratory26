using AppCore.Models.Enums; // Import enumów, m.in. ContactStatus

// Piotr Bacior - WSEI Kraków

namespace AppCore.Models
{
    // Klasa abstrakcyjna Contact — nie można utworzyć jej instancji.
    // Służy jako wspólna baza dla różnych typów kontaktów (np. Person, Company).
    public abstract class Contact : EntityBase
    {
        // Adres e-mail kontaktu — wymagany (string bez '?').
        public string Email { get; set; }

        // Numer telefonu — wymagany.
        public string Phone { get; set; }

        // Adres powiązany z kontaktem — wymagany obiekt Address.
        public Address Address { get; set; }

        // Data utworzenia kontaktu — ustawiana przy tworzeniu.
        public DateTime CreatedAt { get; set; }

        // Data ostatniej aktualizacji — opcjonalna (może być null).
        public DateTime? UpdatedAt { get; set; }

        // Status kontaktu — enum (np. Active, Inactive, Archived).
        public ContactStatus Status { get; set; }

        // Lista tagów przypisanych do kontaktu (np. VIP, Lead, Partner).
        public List<Tag> Tags { get; set; }

        // Lista notatek powiązanych z kontaktem.
        // Brak inicjalizacji — może prowadzić do null, jeśli nie ustawisz w konstruktorze.
        public List<Note> Notes;

        // Abstrakcyjna metoda — każda klasa dziedzicząca musi ją zaimplementować.
        // Zwraca nazwę wyświetlaną kontaktu (np. imię i nazwisko lub nazwę firmy).
        public abstract string GetDisplayName();
    }
}
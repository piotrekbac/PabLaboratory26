using AppCore.DTOs;
using AppCore.Models.Enums;         // Import enumów, m.in. Gender

// Piotr Bacior - WSEI Kraków

namespace AppCore.Models
{
    // Person dziedziczy po Contact, więc ma Email, Phone, Address, Status, Tags, Notes itd.
    public class Person : Contact
    {
        // Imię — wymagane, inicjalizowane pustym stringiem
        public string FirstName { get; set; } = string.Empty;

        // Nazwisko — wymagane, inicjalizowane pustym stringiem
        public string LastName { get; set; } = string.Empty;

        // Drugie imię — opcjonalne
        public string? MiddleName { get; set; }

        // Data urodzenia — opcjonalna
        public DateTime? BirthDate { get; set; }

        // Płeć — enum (np. Male, Female, Other)
        public Gender Gender { get; set; }

        // Stanowisko (np. Manager, Developer) — opcjonalne
        public string? Position { get; set; }

        // Organizacja, do której osoba należy — opcjonalna
        public Organization? Organization { get; set; }

        // Firma, w której osoba pracuje — opcjonalna
        public Company? Employer { get; set; }
        
        // Konwersja na DTO (Data Transfer Object) -> przekształca encję na PersonDto
        public PersonDto ToDto() => new()
        {
            Id = this.Id,
            FirstName = this.FirstName,
            LastName = this.LastName,
            Email = this.Email,
            Phone = this.Phone,
            Position = this.Position,
            BirthDate = this.BirthDate,
            Gender = this.Gender,
            EmployerId = this.Employer?.Id
        };
        
        // Nadpisanie metody z Contact — zwraca imię i nazwisko
        public override string GetDisplayName() => $"{FirstName} {LastName}";
    }
}
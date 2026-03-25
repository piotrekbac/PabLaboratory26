using AppCore.DTOs;
using AppCore.Models.Enums;         // Import enumów, m.in. Gender

// Piotr Bacior - WSEI Kraków

namespace AppCore.Models
{
    // Encja Person dziedziczy po Contact.
    public class Person : Contact
    {
        public string FirstName { get; set; } = string.Empty; // imię
        public string LastName { get; set; } = string.Empty;  // nazwisko
        public string? MiddleName { get; set; }               // drugie imię
        public DateTime? BirthDate { get; set; }              // data urodzenia
        public Gender Gender { get; set; }                    // płeć
        public string? Position { get; set; }                 // stanowisko

        public Organization? Organization { get; set; }       // organizacja
        public Company? Employer { get; set; }                // firma

        // Konwersja encji → DTO
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

        // Wyświetlana nazwa osoby
        public override string GetDisplayName() => $"{FirstName} {LastName}";
    }
}
using AppCore.DTOs;
using AppCore.Models.Enums;

// Piotr Bacior - WSEI Kraków

namespace AppCore.Models
{
    public class Person : Contact
    {
        public string FirstName { get; set; } = string.Empty; // Imię
        public string LastName { get; set; } = string.Empty;  // Nazwisko
        public string? MiddleName { get; set; }               // Drugie imię
        public DateTime? BirthDate { get; set; }              // Data urodzenia
        public Gender Gender { get; set; }                    // Płeć
        public string? Position { get; set; }                 // Stanowisko

        // Klucze obce dla relacji
        public Guid? OrganizationId { get; set; }
        public Organization? Organization { get; set; }       // Organizacja
        
        public Guid? EmployerId { get; set; }
        public Company? Employer { get; set; }                // Firma

        // Konwersja encji na DTO
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
            EmployerId = this.EmployerId
        };

        public override string GetDisplayName() => $"{FirstName} {LastName}";
    }
}
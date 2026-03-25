using AppCore.Models;           // Wymagane dla ToEntity()
using AppCore.Models.Enums;

// Piotr Bacior - WSEI Kraków

namespace AppCore.DTOs;

// DTO reprezentujący osobę zwracaną na zewnątrz.
public record PersonDto : ContactBaseDto
{
    public string FirstName { get; init; } = string.Empty; // Imię
    public string LastName { get; init; } = string.Empty;  // Nazwisko
    public string? Position { get; init; }                 // Stanowisko
    public DateTime? BirthDate { get; init; }              // Data urodzenia
    public Gender Gender { get; init; }                    // Płeć
    public Guid? EmployerId { get; init; }                 // Firma zatrudniająca
    public List<NoteDto> Notes { get; init; } = new();     // Lista notatek
}

// DTO używane przy tworzeniu osoby.
public record CreatePersonDto(
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    string? Position,
    DateTime? BirthDate,
    Gender Gender,
    Guid? EmployerId,
    AddressDto? Address
)
{
    // Konwersja DTO → encja Person
    public Person ToEntity() => new()
    {
        Id = Guid.NewGuid(),
        FirstName = this.FirstName,
        LastName = this.LastName,
        Email = this.Email,
        Phone = this.Phone,
        Position = this.Position,
        BirthDate = this.BirthDate,
        Gender = this.Gender
    };
}

// DTO używane przy aktualizacji osoby.
public record UpdatePersonDto(
    string? FirstName,
    string? LastName,
    string? Email,
    string? Phone,
    string? Position,
    DateTime? BirthDate,
    Gender? Gender,
    Guid? EmployerId,
    AddressDto? Address,
    ContactStatus? Status
);
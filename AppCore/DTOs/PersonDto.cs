using AppCore.Models;           // Wymagane dla metody ToEntity()
using AppCore.Models.Enums;

// Piotr Bacior - WSEI Kraków

namespace AppCore.DTOs;

// DTO reprezentujący osobę zwracaną na zewnątrz.
public record PersonDto : ContactBaseDto
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string? Position { get; init; }
    public DateTime? BirthDate { get; init; }
    public Gender Gender { get; init; }
    public Guid? EmployerId { get; init; }
    public List<NoteDto> Notes { get; init; } = new(); 
}

// DTO używane przy tworzeniu nowej osoby.
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
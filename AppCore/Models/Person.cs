using AppCore.Models.Enums;

namespace AppCore.Models; 

public class Person : Contact   // Person dziedziczy po Contact
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public DateTime? BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string? Position { get; set; }
    public Organization? Organization { get; set; }     // Zależność z diagramu
    public Company? Employer { get; set; }              // Zależność z diagramu

    public override string GetDisplayName() => $"{FirstName} {LastName}";
}
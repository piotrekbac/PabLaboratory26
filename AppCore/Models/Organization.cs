using AppCore.Models.Enums;

namespace AppCore.Models;

public class Organization : Contact
{
    public string Name { get; set; }
    public OrganizationType Type { get; set; }
    public string? KRS { get; set; }
    public string? Website { get; set; }
    public string? Mission { get; set; }
    public List<Person> Members { get; set; }
    public Person? PrimaryContatn { get; set; }
    
    public override string GetDisplayName() => Name;
}
using Microsoft.AspNetCore.Identity;

// Piotr Bacior - WSEI Kraków

namespace Infrastructure.EntityFramework.Entities;

// Rola systemowa oparta na IdentityRole — rozszerzona o opis
public class CrmRole : IdentityRole
{
    public string? Description { get; set; }   // Opcjonalny opis roli

    public CrmRole() { }                       // Konstruktor domyślny

    public CrmRole(string roleName, string? description = null) : base(roleName)
    {
        Description = description;             // Ustawienie opisu roli
    }
}
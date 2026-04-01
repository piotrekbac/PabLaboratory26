using AppCore.Interfaces;
using Microsoft.AspNetCore.Identity;

// Piotr Bacior - WSEI Kraków

namespace Infrastructure.EntityFramework.Entities;

// Użytkownik CRM — rozszerzenie IdentityUser o dane domenowe
public class CrmUser : IdentityUser, ISystemUser
{
    public required string FirstName { get; set; }        // Imię użytkownika
    public required string LastName { get; set; }         // Nazwisko użytkownika
    public required string FullName { get; set; }         // Pełne imię i nazwisko
    // Email dziedziczony z IdentityUser
    public required string Department { get; set; }       // Dział użytkownika
    public required SystemUserStatus Status { get; set; } // Status konta
    public DateTime CreatedAt { get; set; }               // Data utworzenia konta
    public DateTime? LastLoginAt { get; private set; }    // Ostatnie logowanie
    public DateTime? DeactivatedAt { get; private set; }  // Data dezaktywacji

    // Aktywacja konta — zmiana statusu na Active
    public void Activate()
    {
        if (Status == SystemUserStatus.Inactive) Status = SystemUserStatus.Active;
    }

    // Dezaktywacja konta — zapis daty i zmiana statusu
    public void Deactivate(DateTime now)
    {
        if (Status == SystemUserStatus.Active)
        {
            Status = SystemUserStatus.Inactive;
            DeactivatedAt = now;
        }
    }
}
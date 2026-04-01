namespace AppCore.Interfaces;

// Piotr Bacior - WSEI Kraków

// Status użytkownika systemowego — określa jego bieżący stan
public enum SystemUserStatus { Active, Inactive, Locked, PendingActivation }

// Role użytkowników — definiują poziom uprawnień w systemie
public enum UserRole { Administrator, SalesManager, Salesperson, SupportAgent, ReadOnly }

public interface ISystemUser
{
    string Id { get; }                 // Unikalny identyfikator użytkownika
    string Email { get; }              // Email logowania
    string FirstName { get; }          // Imię użytkownika
    string LastName { get; }           // Nazwisko użytkownika
    string FullName { get; }           // Pełne imię i nazwisko
    string Department { get; }         // Dział, do którego należy użytkownik
    SystemUserStatus Status { get; }   // Aktualny status konta
    DateTime CreatedAt { get; }        // Data utworzenia konta
}
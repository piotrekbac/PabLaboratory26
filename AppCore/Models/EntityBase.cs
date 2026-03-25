namespace AppCore.Models;

// Piotr Bacior - WSEI Kraków

public abstract class EntityBase
{
    // Automatyczne generowanie GUID przy tworzeniu
    public Guid Id { get; set; } = Guid.NewGuid();
}
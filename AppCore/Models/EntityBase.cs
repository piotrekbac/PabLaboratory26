namespace AppCore.Models;

public abstract class EntityBase
{
    // Automatyczne generowanie GUID przy tworzeniu
    public Guid Id { get; set; } = Guid.NewGuid();
}
namespace AppCore.Models;

// Piotr Bacior - WSEI Kraków

// Bazowa klasa encji — każda encja ma automatycznie generowany GUID.
public abstract class EntityBase
{
    public Guid Id { get; set; }
}
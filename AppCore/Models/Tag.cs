namespace AppCore.Models;

public class Tag : EntityBase
{
    public Guid id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
}
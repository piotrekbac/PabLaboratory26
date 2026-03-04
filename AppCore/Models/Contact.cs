using AppCore.Models.Enums; 

namespace AppCore.Models;

public abstract class Contact : EntityBase
{
    public string Email { get; set; }
    public string Phone { get; set; }
    public Address Address { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ContactStatus Status { get; set; }
    public List<Tag> Tags { get; set; }
    public List<Note> Notes;
    
    public abstract string GetDisplayName();
}
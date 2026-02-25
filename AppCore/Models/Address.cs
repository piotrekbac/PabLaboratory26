using AppCore.ValueObjects;

namespace AppCore.Models;

public class Address
{
    // Ponieważ może być dwóch klientów zamawiających z tego samego adresu dodajemy tożsamość
    public int Id { get; set; }
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string ZipCode { get; set; }
    public required Country Country { get; set; }
    
    // Dodajemy required, aby każde z tych pól było wymagane
}
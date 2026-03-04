using AppCore.ValueObjects;

namespace AppCore.Models;

public class Address1
{
    // Ponieważ może być dwóch klientów zamawiających z tego samego adresu dodajemy tożsamość
    // Dodajemy required, aby każde z tych pól było wymagane
    
    public int Id { get; set; }
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string ZipCode { get; set; }
    public required Country Country { get; set; }
    
    // Dodajemy required, aby każde z tych pól było wymagane
}

// podczas labolatorium2 powiedziano, że można usunąć ten interfejs, ale ja go zostawiam podglądowo. 
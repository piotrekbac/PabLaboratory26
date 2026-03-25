namespace AppCore.Models;

// Piotr Bacior - WSEI Kraków

public class Customer1
{
    // prop (i podpowiada dalej)
    public int Id { get; set; }
    
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required int AddressId { get; set; }
    
    // Będziemy mogli się wspomóc biblioteką Identity (.NET)
    // public string Password { get; set; }
}

// podczas labolatorium2 powiedziano, że można usunąć ten interfejs, ale ja go zostawiam podglądowo. 
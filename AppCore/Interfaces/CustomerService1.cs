using AppCore.Models;

// Piotr Bacior - WSEI Kraków

namespace AppCore.Interfaces;

public interface ICustomerService
{
    // Metoda synchroniczna
    public IEnumerable<Customer1> GetCustomers();
    
    // Metoda Asynchroniczna 
    public Task<IEnumerable<Customer1>> GetCustomersAsync();
}

// podczas labolatorium2 powiedziano, że można usunąć ten interfejs, ale ja go zostawiam podglądowo. 
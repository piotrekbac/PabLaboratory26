using AppCore.Models;

// Piotr Bacior - WSEI Kraków

namespace AppCore.Interfaces;

// Interfejs przykładowej usługi klientów.
// Pozostawiony poglądowo — pokazuje różnicę między metodami synchronicznymi i asynchronicznymi.
public interface ICustomerService
{
    // Metoda synchroniczna — zwraca listę klientów.
    public IEnumerable<Customer1> GetCustomers();
    
    // Metoda asynchroniczna — zwraca listę klientów w Tasku.
    public Task<IEnumerable<Customer1>> GetCustomersAsync();
}

// podczas laboratorium 2 powiedziano, że można usunąć ten interfejs, ale ja go zostawiam podglądowo.
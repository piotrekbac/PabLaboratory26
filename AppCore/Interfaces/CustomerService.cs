using AppCore.Models;

namespace AppCore.Interfaces;

public interface ICustomerService
{
    // Metoda synchroniczna
    public IEnumerable<Customer> GetCustomers();
    
    // Metoda Asynchroniczna 
    public Task<IEnumerable<Customer>> GetCustomersAsync();
}
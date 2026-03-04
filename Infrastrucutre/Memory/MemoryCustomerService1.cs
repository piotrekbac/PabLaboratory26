using AppCore.Interfaces;
using AppCore.Models;

namespace Infrastrucutre.Memory;

public class MemoryCustomerService1 : ICustomerService
{
    public IEnumerable<Customer1> GetCustomers()
    {
        return [
            new Customer1()
            {
                Id = 1,
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "a@wsei.edu.pl",
                Phone = "111-222-333",
                AddressId = 11
            },
            new Customer1
            {
                Id = 2,
                FirstName = "Anna",
                LastName = "Nowak",
                Email = "b@wsei.edu.pl",
                Phone = "444-555-666",
                AddressId = 22
            }
        ];
    }

    // podczas labolatorium2 powiedziano, że można usunąć ten interfejs, ale ja go zostawiam podglądowo. 
    
    public Task<IEnumerable<Customer1>> GetCustomersAsync()
    {
        throw new NotImplementedException();
    }
}
using AppCore.Interfaces;
using AppCore.Models;
using Interfaces.Memory;

// Piotr Bacior - WSEI Kraków

namespace Infrastrucutre.Memory;

// Repozytorium pamięciowe dla Company — implementacja poglądowa
public class MemoryCompanyRepository : MemoryGenericRepository<Company>, ICompanyRepository
{
    public Task<IEnumerable<Company>> FindByNameAsync(string nameQuery) => throw new NotImplementedException(); // Wyszukiwanie po nazwie
    public Task<Company?> FindByNipAsync(string nip) => throw new NotImplementedException();                    // Wyszukiwanie po NIP
    public Task<IEnumerable<Person>> GetEmployeesAsync(Guid companyId) => throw new NotImplementedException();  // Pobieranie pracowników
}
using AppCore.Interfaces;
using AppCore.Models;
using Interfaces.Memory;

// Piotr Bacior - WSEI Kraków

namespace Infrastrucutre.Memory;

// Repozytorium pamięciowe dla Company — rozszerza generyczne repozytorium
public class CompanyRepository : MemoryGenericRepository<Company>, ICompanyRepository
{
    // Wyszukiwanie firm po fragmencie nazwy
    public async Task<IEnumerable<Company>> FindByNameAsync(string nameQuery)
    {
        return await Task.FromResult(
            _data.Values.Where(c => c.Name.Contains(nameQuery, StringComparison.OrdinalIgnoreCase))
        );
    }

    // Wyszukiwanie firmy po NIP
    public async Task<Company?> FindByNipAsync(string nip)
    {
        return await Task.FromResult(
            _data.Values.FirstOrDefault(c => c.NIP == nip)
        );
    }

    // Pobieranie pracowników firmy
    public async Task<IEnumerable<Person>> GetEmployeesAsync(Guid companyId)
    {
        var company = await FindByIdAsync(companyId);
        return company?.Employees ?? Enumerable.Empty<Person>();
    }
}
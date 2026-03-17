using AppCore.Interfaces;
using AppCore.Models;
using Interfaces.Memory;

namespace Infrastrucutre.Memory;

public class CompanyRepository : MemoryGenericRepository<Company>, ICompanyRepository
{
    public async Task<IEnumerable<Company>> FindByNameAsync(string nameQuery)
    {
        // Wykorzystujemy protected _data z klasy bazowej MemoryGenericRepository
        return await Task.FromResult(_data.Values.Where(c => c.Name.Contains(nameQuery, StringComparison.OrdinalIgnoreCase)));
    }

    public async Task<Company?> FindByNipAsync(string nip)
    {
        return await Task.FromResult(_data.Values.FirstOrDefault(c => c.NIP == nip));
    }

    public async Task<IEnumerable<Person>> GetEmployeesAsync(Guid companyId)
    {
        // Pobieramy wszystkich z PersonRepository (wymagałoby to wstrzyknięcia IPersonRepository)
        // LUB jeśli trzymamy listę pracowników w klasie Company:
        var company = await FindByIdAsync(companyId);
        return company?.Employees ?? Enumerable.Empty<Person>();
    }
}
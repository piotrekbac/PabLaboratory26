using AppCore.Interfaces;
using AppCore.Models;
using Interfaces.Memory;

namespace Infrastrucutre.Memory;

public class MemoryCompanyRepository : MemoryGenericRepository<Company>, ICompanyRepository
{
    public Task<IEnumerable<Company>> FindByNameAsync(string nameQuery) => throw new NotImplementedException();
    public Task<Company?> FindByNipAsync(string nip) => throw new NotImplementedException();
    public Task<IEnumerable<Person>> GetEmployeesAsync(Guid companyId) => throw new NotImplementedException();
}
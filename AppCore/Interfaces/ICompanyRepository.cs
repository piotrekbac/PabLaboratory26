using AppCore.Models;

namespace AppCore.Interfaces;

public interface ICompanyRepository : IGenericRepositoryAsync<Company>
{
    Task<IEnumerable<Company>> FindByNameAsync(string nameQuery);
    Task<Company?> FindByNipAsync(string NIP);
    Task<IEnumerable<Person>> GetEmployeesAsync(Guid companyId);
}


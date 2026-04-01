using AppCore.Models;

// Piotr Bacior - WSEI Kraków

namespace AppCore.Interfaces
{
    // Repozytorium dla encji Person
    public interface IPersonRepository : IGenericRepositoryAsync<Person>
    {
        // Zwraca osoby zatrudnione w firmie
        Task<IEnumerable<Person>> GetEmployeesByCompanyAsync(Guid companyId);

        // Zwraca osoby będące członkami organizacji
        Task<IEnumerable<Person>> GetMembersByOrganizationAsync(Guid organizationId);
    }
}
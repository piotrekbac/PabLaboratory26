using AppCore.Models;

// Piotr Bacior - WSEI Kraków

namespace AppCore.Interfaces
{
    // Repozytorium dla encji Person.
    // Dziedziczy po generycznym repozytorium asynchronicznym, więc automatycznie ma metody takie jak:
    // AddAsync, GetByIdAsync, ListAsync, UpdateAsync, DeleteAsync itd.
    public interface IPersonRepository : IGenericRepositoryAsync<Person>
    {
        // Zwraca listę osób zatrudnionych w firmie o podanym companyId.
        // companyId to Guid identyfikujący encję Company.
        Task<IEnumerable<Person>> GetEmployeesByCompanyAsync(Guid companyId);

        // Zwraca listę osób będących członkami organizacji o podanym organizationId.
        // organizationId to Guid identyfikujący encję Organization.
        Task<IEnumerable<Person>> GetMembersByOrganizationAsync(Guid organizationId);
    }
}
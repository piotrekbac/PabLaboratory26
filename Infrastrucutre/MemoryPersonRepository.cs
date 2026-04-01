using AppCore.Interfaces;
using AppCore.Models;
using AppCore.Models.Enums;
using Interfaces.Memory;

// Piotr Bacior - WSEI Kraków

namespace Infrastrucutre.Memory;

// Repozytorium pamięciowe dla Person
public class MemoryPersonRepository : MemoryGenericRepository<Person>, IPersonRepository
{
    public MemoryPersonRepository() : base()
    {
        // Dodajemy przykładową osobę
        var id = Guid.NewGuid();
        _data.Add(id, new Person()
        {
            Id = id,
            FirstName = "Adam",
            LastName = "Nowak",
            Gender = Gender.Male,
            Email = "adam.nowak@example.com"
        });
    }

    // Pobieranie pracowników firmy
    public async Task<IEnumerable<Person>> GetEmployeesByCompanyAsync(Guid companyId)
    {
        return await Task.FromResult(
            _data.Values.Where(p => p.Employer != null && p.Employer.Id == companyId)
        );
    }

    // Pobieranie członków organizacji
    public Task<IEnumerable<Person>> GetMembersByOrganizationAsync(Guid organizationId)
    {
        throw new NotImplementedException();
    }
}
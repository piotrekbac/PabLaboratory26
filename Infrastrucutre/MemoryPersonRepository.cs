using AppCore.Interfaces;
using AppCore.Models;
using AppCore.Models.Enums;
using Interfaces.Memory;

namespace Infrastrucutre.Memory; 

public class MemoryPersonRepository : MemoryGenericRepository<Person>, IPersonRepository
{
    public MemoryPersonRepository() : base()
    {
        // Dodajemy przykładowe dane
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

    public async Task<IEnumerable<Person>> GetEmployeesByCompanyAsync(Guid companyId)
    {
        // Używamy pola _data (teraz protected) z klasy bazowej
        return await Task.FromResult(_data.Values.Where(p => p.Employer != null && p.Employer.Id == companyId));
    }

    public Task<IEnumerable<Person>> GetMembersByOrganizationAsync(Guid organizationId)
    {
        throw new NotImplementedException();
    }
}
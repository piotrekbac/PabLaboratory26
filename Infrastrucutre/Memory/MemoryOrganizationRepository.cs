using AppCore.Interfaces;
using AppCore.Models;
using AppCore.Models.Enums;
using Interfaces.Memory;

// Piotr Bacior - WSEI Kraków

namespace Infrastrucutre.Memory;

// Repozytorium pamięciowe dla Organization
public class MemoryOrganizationRepository : MemoryGenericRepository<Organization>, IOrganizationRepository
{
    // Pobieranie organizacji po typie
    public Task<IEnumerable<Organization>> GetByTypeAsync(OrganizationType type) 
        => Task.FromResult(_data.Values.Where(o => o.Type == type));

    // Pobieranie członków organizacji
    public Task<IEnumerable<Person>> GetMembersAsync(Guid organizationId) 
        => Task.FromResult(
            _data.TryGetValue(organizationId, out var org)
                ? org.Members.AsEnumerable()
                : Enumerable.Empty<Person>()
        );
}
using AppCore.Interfaces;
using AppCore.Models;
using AppCore.Models.Enums;
using Interfaces.Memory;

namespace Infrastrucutre.Memory; // Upewnij się, że nazwa namespace jest identyczna w całym projekcie

public class MemoryOrganizationRepository : MemoryGenericRepository<Organization>, IOrganizationRepository
{
    public Task<IEnumerable<Organization>> GetByTypeAsync(OrganizationType type) 
        => Task.FromResult(_data.Values.Where(o => o.Type == type));

    public Task<IEnumerable<Person>> GetMembersAsync(Guid organizationId) 
        => Task.FromResult(_data.TryGetValue(organizationId, out var org) ? org.Members.AsEnumerable() : Enumerable.Empty<Person>());
}
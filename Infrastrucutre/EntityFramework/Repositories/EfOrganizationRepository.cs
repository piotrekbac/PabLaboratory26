using AppCore.Interfaces;
using AppCore.Models;
using AppCore.Models.Enums;
using Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

// Piotr Bacior - WSEI Kraków

namespace Infrastructure.EntityFramework.Repositories;

// Repozytorium EF dla Organization — operacje na organizacjach
public class EfOrganizationRepository(ContactsDbContext context) : 
    EfGenericRepository<Organization>(context.Organizations), 
    IOrganizationRepository
{
    // Pobieranie organizacji po typie (np. NGO, Firma)
    public async Task<IEnumerable<Organization>> GetByTypeAsync(OrganizationType type)
    {
        return await context.Organizations
            .Where(o => o.Type == type)
            .ToListAsync();
    }

    // Pobieranie członków organizacji
    public async Task<IEnumerable<Person>> GetMembersAsync(Guid organizationId)
    {
        var org = await context.Organizations
            .Include(o => o.Members)
            .FirstOrDefaultAsync(o => o.Id == organizationId);
        
        return org?.Members ?? Enumerable.Empty<Person>();
    }
}
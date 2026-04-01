using AppCore.Interfaces;
using AppCore.Models;
using AppCore.Models.Enums;
using Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

// Piotr Bacior - WSEI Kraków

namespace Infrastructure.EntityFramework.Repositories
{
    public class EfOrganizationRepository(ContactsDbContext context) : 
        EfGenericRepository<Organization>(context.Organizations), 
        IOrganizationRepository
    {
        public async Task<IEnumerable<Organization>> GetByTypeAsync(OrganizationType type)
        {
            return await context.Organizations
                .Where(o => o.Type == type)
                .ToListAsync();
        }

        public async Task<IEnumerable<Person>> GetMembersAsync(Guid organizationId)
        {
            var org = await context.Organizations
                .Include(o => o.Members)
                .FirstOrDefaultAsync(o => o.Id == organizationId);
            
            return org?.Members ?? Enumerable.Empty<Person>();
        }
    }
}
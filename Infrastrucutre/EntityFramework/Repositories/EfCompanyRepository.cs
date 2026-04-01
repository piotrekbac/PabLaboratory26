using AppCore.Interfaces;
using AppCore.Models;
using Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

// Piotr Bacior - WSEI Kraków

namespace Infrastructure.EntityFramework.Repositories
{
    // Dziedziczy po EfGenericRepository, dzięki czemu ma CRUD za darmo
    public class EfCompanyRepository(ContactsDbContext context) : 
        EfGenericRepository<Company>(context.Companies), 
        ICompanyRepository
    {
        // Wyszukiwanie firmy po numerze NIP
        public async Task<Company?> FindByNipAsync(string nip)
        {
            return await context.Companies.FirstOrDefaultAsync(c => c.NIP == nip);
        }

        // Wyszukiwanie firm po nazwie (z użyciem SQL LIKE)
        public async Task<IEnumerable<Company>> FindByNameAsync(string name)
        {
            return await context.Companies
                .Where(c => c.Name.Contains(name))
                .ToListAsync();
        }

        // Pobieranie pracowników danej firmy
        public async Task<IEnumerable<Person>> GetEmployeesAsync(Guid companyId)
        {
            return await context.People
                .Where(p => p.Employer != null && p.Employer.Id == companyId)
                .ToListAsync();
        }
    }
}
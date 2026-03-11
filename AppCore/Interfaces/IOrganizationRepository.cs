using AppCore.Models;
using AppCore.Models.Enums;

namespace AppCore.Interfaces
{
    // Repozytorium dla encji Organization.
    // Dziedziczy po generycznym repozytorium asynchronicznym więc ma już podstawowe operacje CRUD (Add, Update, Delete, GetById, List itd.).
    public interface IOrganizationRepository : IGenericRepositoryAsync<Organization>
    {
        // Zwraca wszystkie organizacje o określonym typie (np. NGO, Foundation, Association).
        public async Task<IEnumerable<Organization>> GetByTypeAsync(OrganizationType type)
        {
            var all = await FindAllAsync();
            return all.Where(o => o.Type == type);
        }
        

        // Zwraca listę członków organizacji o podanym identyfikatorze.
        // organizationId to Guid identyfikujący encję Organization.
        public async Task AddNoteAsync(Guid contactId, Note note)
        {
            var contact = await FindByIdAsync(contactId);
            if (contact != null)
            {
                contact.Notes.Add(note);
            }
        }
        
        
    }
    
    
}
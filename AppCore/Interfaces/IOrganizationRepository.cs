using AppCore.Models;
using AppCore.Models.Enums;

// Piotr Bacior - WSEI Kraków

namespace AppCore.Interfaces
{
    // Repozytorium dla encji Organization
    public interface IOrganizationRepository : IGenericRepositoryAsync<Organization>
    {
        // Zwraca organizacje o określonym typie (np. NGO, Foundation)
        public async Task<IEnumerable<Organization>> GetByTypeAsync(OrganizationType type)
        {
            var all = await FindAllAsync();
            return all.Where(o => o.Type == type);
        }

        // Dodaje notatkę do organizacji (działa podobnie jak w Contact)
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
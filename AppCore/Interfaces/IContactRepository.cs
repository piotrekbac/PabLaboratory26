using AppCore.DTOs;
using AppCore.Models;

// Piotr Bacior - WSEI Kraków

namespace AppCore.Interfaces
{
    // Repozytorium dla encji Contact
    // Dziedziczy po generycznym repozytorium, więc ma podstawowe operacje CRUD
    public interface IContactRepository : IGenericRepositoryAsync<Contact>
    {
        // Wyszukiwanie kontaktów na podstawie kryteriów z DTO
        Task<PagedResult<Contact>> SearchAsync(ContactSearchDto searchDto);

        // Zwraca wszystkie kontakty posiadające dany tag
        Task<IEnumerable<Contact>> FindByTagAsync(string tag);

        // Dodaje notatkę do kontaktu
        Task AddNoteAsync(Guid contactId, Note note);

        // Pobiera wszystkie notatki kontaktu
        Task<IEnumerable<Note>> GetNotesAsync(Guid contactId);

        // Dodaje tag do kontaktu
        Task AddTagAsync(Guid contactId, Tag tag);

        // Usuwa tag z kontaktu
        Task RemoveTagAsync(Guid contactId, Guid tagId);
    }
}
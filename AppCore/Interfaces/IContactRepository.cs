using AppCore.DTOs;
using AppCore.Models;

// Piotr Bacior - WSEI Kraków

namespace AppCore.Interfaces
{
    // Repozytorium dla encji Contact. Dziedziczy po generycznym repozytorium, więc ma już:
    // FindByIdAsync, FindAllAsync, FindPagedAsync, AddAsync, UpdateAsync, RemoveByIdAsync.
    public interface IContactRepository : IGenericRepositoryAsync<Contact>
    {
        // Wyszukiwanie kontaktów na podstawie kryteriów z DTO.
        // Zwraca wyniki w formie stronicowanej.
        Task<PagedResult<Contact>> SearchAsync(ContactSearchDto searchDto);

        // Zwraca wszystkie kontakty, które mają przypisany tag o podanej nazwie.
        Task<IEnumerable<Contact>> FindByTagAsync(string tag);

        // Dodaje notatkę do kontaktu o podanym ID.
        Task AddNoteAsync(Guid contactId, Note note);

        // Pobiera wszystkie notatki przypisane do kontaktu.
        Task<IEnumerable<Note>> GetNotesAsync(Guid contactId);

        // Dodaje tag do kontaktu.
        Task AddTagAsync(Guid contactId, Tag tag);

        // Usuwa tag z kontaktu na podstawie jego ID.
        Task RemoveTagAsync(Guid contactId, Guid tagId);
    }
}
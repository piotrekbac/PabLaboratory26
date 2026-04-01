using AppCore.DTOs;

// Piotr Bacior - WSEI Kraków

namespace AppCore.Interfaces
{
    // Ogólny interfejs repozytorium asynchronicznego
    // T — dowolna encja domenowa
    public interface IGenericRepositoryAsync<T> where T : class
    {
        // Pobiera encję po ID
        Task<T?> FindByIdAsync(Guid id);

        // Pobiera wszystkie encje
        Task<IEnumerable<T>> FindAllAsync();

        // Pobiera encje w sposób stronicowany
        Task<PagedResult<T>> FindPagedAsync(int page, int pageSize);

        // Dodaje nową encję
        Task<T> AddAsync(T entity);

        // Aktualizuje istniejącą encję
        Task<T> UpdateAsync(T entity);

        // Usuwa encję po ID
        Task RemoveByIdAsync(Guid id);
    }
}
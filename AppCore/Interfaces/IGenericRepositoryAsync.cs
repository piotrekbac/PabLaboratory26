using AppCore.DTOs;

namespace AppCore.Interfaces
{
    // Ogólny interfejs repozytorium asynchronicznego.
    // T oznacza dowolny typ encji (np. Person, Company, Organization).
    public interface IGenericRepositoryAsync<T> where T : class
    {
        // Pobiera encję po identyfikatorze. Zwraca null, jeśli nie istnieje.
        Task<T?> FindByIdAsync(Guid id);

        // Pobiera wszystkie encje danego typu.
        Task<IEnumerable<T>> FindAllAsync();

        // Pobiera encje w sposób stronicowany (paginacja).
        // Zwraca obiekt PagedResult<T> zawierający dane i metadane paginacji.
        Task<PagedResult<T>> FindPagedAsync(int page, int pageSize);

        // Dodaje nową encję do repozytorium i zwraca ją po zapisaniu.
        Task<T> AddAsync(T entity);

        // Aktualizuje istniejącą encję i zwraca jej zaktualizowaną wersję.
        Task<T> UpdateAsync(T entity);

        // Usuwa encję po identyfikatorze.
        Task RemoveByIdAsync(Guid id);
    }
}
using AppCore.DTOs;
using AppCore.Interfaces;
using AppCore.Models;
using Microsoft.EntityFrameworkCore;

// Piotr Bacior - WSEI Kraków

namespace Infrastructure.EntityFramework.Repositories
{
    // Repozytorium generyczne dla EF Core.
    // Wymaga, aby encja dziedziczyła po EntityBase
    public class EfGenericRepository<T>(DbSet<T> set) : IGenericRepositoryAsync<T> 
        where T : EntityBase
    {
        // Pobranie encji po unikalnym ID
        public async Task<T?> FindByIdAsync(Guid id) => await set.FindAsync(id);

        // Pobranie wszystkich elementów z tabeli
        public async Task<IEnumerable<T>> FindAllAsync() => await set.ToListAsync();

        // Pobieranie danych z podziałem na strony (stronicowanie)
        public async Task<PagedResult<T>> FindPagedAsync(int page, int pageSize)
        {
            var totalCount = await set.CountAsync();
            var items = await set
                .AsNoTracking()     // Zwiększa wydajność, bo nie śledzimy zmian
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<T>(items, totalCount, page, pageSize);
        }

        // Dodawanie nowej encji do bazy
        public async Task<T> AddAsync(T entity)
        {
            var entry = await set.AddAsync(entity);
            return entry.Entity;
        }

        // Aktualizacja istniejącej encji
        public Task<T> UpdateAsync(T entity)
        {
            var entityEntry = set.Update(entity);
            return Task.FromResult(entityEntry.Entity);
        }

        // Usuwanie encji po ID
        public async Task RemoveByIdAsync(Guid id)
        {
            var entity = await set.FindAsync(id);
            if (entity != null)
            {
                set.Remove(entity);
            }
        }
    }
}
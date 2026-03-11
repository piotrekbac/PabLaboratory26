using AppCore.DTOs;
using AppCore.Interfaces;
using AppCore.Models;

namespace Interfaces.Memory;

public class MemoryGenericRepository<T> : IGenericRepositoryAsync<T> 
    where T : EntityBase
{
    // Zmieniono na protected, aby repozytoria konkretne miały dostęp do danych - WAŻNY KROK Z INSTRUKCJI
    protected readonly Dictionary<Guid, T> _data = new();

    public Task<T?> FindByIdAsync(Guid id)
    {
        _data.TryGetValue(id, out var value);
        return Task.FromResult(value);
    }

    public Task<IEnumerable<T>> FindAllAsync()
    {
        return Task.FromResult(_data.Values.AsEnumerable());
    }

    public Task<PagedResult<T>> FindPagedAsync(int page, int pageSize)
    {
        var items = _data.Values
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PagedResult<T>(items, _data.Count, page, pageSize);
        return Task.FromResult(result);
    }

    public Task<T> AddAsync(T entity)
    {
        _data[entity.Id] = entity;
        return Task.FromResult(entity);
    }

    public Task<T> UpdateAsync(T entity)
    {
        if (_data.ContainsKey(entity.Id))
        {
            _data[entity.Id] = entity;
            return Task.FromResult(entity);
        }
        throw new KeyNotFoundException($"Encja o ID {entity.Id} nie istnieje.");
    }

    public Task RemoveByIdAsync(Guid id)
    {
        if (_data.Remove(id))
        {
            return Task.CompletedTask;
        }
        throw new KeyNotFoundException($"Nie można usunąć: encja o ID {id} nie istnieje.");
    }
}
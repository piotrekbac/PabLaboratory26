using AppCore.Interfaces;

// Piotr Bacior - WSEI Kraków

namespace Interfaces.Memory;

// Implementacja Unit of Work działająca w pamięci (np. do testów)
public class MemoryContactUnitOfWork(
    IPersonRepository persons,
    ICompanyRepository companies,
    IOrganizationRepository organizations
) : IContactUnitOfWork
{
    // Repozytoria przekazane przez konstruktor
    public IPersonRepository Persons => persons;
    public ICompanyRepository Companies => companies;
    public IOrganizationRepository Organizations => organizations;

    // Brak zasobów do zwalniania — zwracamy CompletedTask
    public ValueTask DisposeAsync() => ValueTask.CompletedTask;

    // W pamięci nie ma prawdziwego zapisu — zwracamy 0
    public Task<int> SaveChangesAsync() => Task.FromResult(0);

    // Operacje transakcyjne są puste — w pamięci nie mają znaczenia
    public Task BeginTransactionAsync() => Task.CompletedTask;
    public Task CommitTransactionAsync() => Task.CompletedTask;
    public Task RollbackTransactionAsync() => Task.CompletedTask;
}
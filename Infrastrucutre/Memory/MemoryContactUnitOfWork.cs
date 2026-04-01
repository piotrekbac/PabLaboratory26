using AppCore.Interfaces;

// Piotr Bacior - WSEI Kraków

namespace Infrastructure.Memory;

// Jednostka pracy w pamięci — uproszczona implementacja bez transakcji
public class MemoryContactUnitOfWork(
    IPersonRepository persons,
    ICompanyRepository companies,
    IOrganizationRepository organizations
) : IContactUnitOfWork
{
    public IPersonRepository Persons => persons;               // Repozytorium osób
    public ICompanyRepository Companies => companies;          // Repozytorium firm
    public IOrganizationRepository Organizations => organizations; // Repozytorium organizacji

    public ValueTask DisposeAsync() => ValueTask.CompletedTask; // Brak zasobów do zwalniania

    public Task<int> SaveChangesAsync() => Task.FromResult(0); // Brak trwałego zapisu

    public Task BeginTransactionAsync() => Task.CompletedTask; // Brak transakcji
    public Task CommitTransactionAsync() => Task.CompletedTask;
    public Task RollbackTransactionAsync() => Task.CompletedTask;
}
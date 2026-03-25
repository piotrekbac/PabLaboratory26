using AppCore.Interfaces;

// Piotr Bacior - WSEI Kraków

namespace Infrastructure.Memory;

// Jednostka pracy działająca w pamięci — spina repozytoria w jedną transakcję
public class MemoryContactUnitOfWork(
    IPersonRepository persons,
    ICompanyRepository companies,
    IOrganizationRepository organizations
) : IContactUnitOfWork
{
    public IPersonRepository Persons => persons;
    public ICompanyRepository Companies => companies;
    public IOrganizationRepository Organizations => organizations;

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;

    public Task<int> SaveChangesAsync() => Task.FromResult(0);

    public Task BeginTransactionAsync() => Task.CompletedTask;
    public Task CommitTransactionAsync() => Task.CompletedTask;
    public Task RollbackTransactionAsync() => Task.CompletedTask;
}
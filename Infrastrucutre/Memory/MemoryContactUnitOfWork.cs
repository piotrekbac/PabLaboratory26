using AppCore.Interfaces;

// Piotr Bacior - WSEI Kraków

namespace Infrastructure.Memory;

public class MemoryContactUnitOfWork(
    IPersonRepository persons,
    ICompanyRepository companies,
    IOrganizationRepository organizations
) : IContactUnitOfWork
{
    // Dzięki nowej składni C# (Primary Constructor)
    // pola persons, companies, organizations są dostępne od razu.

    public IPersonRepository Persons => persons;
    public ICompanyRepository Companies => companies;
    public IOrganizationRepository Organizations => organizations;

    public ValueTask DisposeAsync()
    {
        return ValueTask.CompletedTask;
    }

    public Task<int> SaveChangesAsync()
    {
        return Task.FromResult(0);
    }

    public Task BeginTransactionAsync() => Task.CompletedTask;
    public Task CommitTransactionAsync() => Task.CompletedTask;
    public Task RollbackTransactionAsync() => Task.CompletedTask;
}
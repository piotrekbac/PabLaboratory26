using AppCore.Interfaces;
using Infrastructure.EntityFramework.Context;

namespace Infrastructure.EntityFramework.UnitOfWork;

public class EfContactsUnitOfWork(
    IPersonRepository persons,
    ICompanyRepository companies,
    IOrganizationRepository organizations,
    ContactsDbContext context) : IContactUnitOfWork
{
    public IPersonRepository Persons => persons;
    public ICompanyRepository Companies => companies;
    public IOrganizationRepository Organizations => organizations;

    public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();

    public async Task BeginTransactionAsync() => await context.Database.BeginTransactionAsync();
    public async Task CommitTransactionAsync() => await context.Database.CommitTransactionAsync();
    public async Task RollbackTransactionAsync() => await context.Database.RollbackTransactionAsync();

    public async ValueTask DisposeAsync() => await context.DisposeAsync();
}
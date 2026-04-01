using AppCore.Interfaces;
using Infrastructure.EntityFramework.Context;

namespace Infrastructure.EntityFramework.UnitOfWork;

// Jednostka pracy EF — spina repozytoria i zarządza transakcjami
public class EfContactsUnitOfWork(
    IPersonRepository persons,
    ICompanyRepository companies,
    IOrganizationRepository organizations,
    ContactsDbContext context) : IContactUnitOfWork
{
    public IPersonRepository Persons => persons;                                                        // Repozytorium osób
    public ICompanyRepository Companies => companies;                                                   // Repozytorium firm
    public IOrganizationRepository Organizations => organizations;                                      // Repozytorium organizacji

    public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();                      // Zapis zmian

    public async Task BeginTransactionAsync() => await context.Database.BeginTransactionAsync();        // Start transakcji
    public async Task CommitTransactionAsync() => await context.Database.CommitTransactionAsync();      // Zatwierdzenie
    public async Task RollbackTransactionAsync() => await context.Database.RollbackTransactionAsync();  // Wycofanie

    public async ValueTask DisposeAsync() => await context.DisposeAsync();                              // Zwolnienie zasobów
}
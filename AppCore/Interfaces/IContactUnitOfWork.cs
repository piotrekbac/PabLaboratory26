using AppCore.Interfaces;

// Piotr Bacior - WSEI Kraków

namespace AppCore.Interfaces;

// Jednostka pracy (Unit of Work) dla kontaktów
// Zapewnia spójne zarządzanie transakcjami i dostępem do repozytoriów
public interface IContactUnitOfWork : IAsyncDisposable
{
    // Repozytoria dostępne w ramach jednej transakcji
    IPersonRepository Persons { get; }
    ICompanyRepository Companies { get; }
    IOrganizationRepository Organizations { get; }

    // Zapis zmian do bazy
    Task<int> SaveChangesAsync();

    // Obsługa transakcji
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
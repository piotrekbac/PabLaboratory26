using AppCore.Models;

// Piotr Bacior - WSEI Kraków

namespace AppCore.Interfaces
{
    // Repozytorium dla encji Company.
    // Dziedziczy po generycznym repozytorium asynchronicznym, więc posiada podstawowe operacje CRUD.
    public interface ICompanyRepository : IGenericRepositoryAsync<Company>
    {
        // Wyszukuje firmy, których nazwa zawiera podany fragment.
        Task<IEnumerable<Company>> FindByNameAsync(string nameQuery);

        // Wyszukuje firmę po numerze NIP — zwraca null, jeśli nie istnieje.
        Task<Company?> FindByNipAsync(string NIP);

        // Pobiera listę pracowników przypisanych do firmy o podanym ID.
        Task<IEnumerable<Person>> GetEmployeesAsync(Guid companyId);
    }
}
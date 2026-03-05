using AppCore.Models;

namespace AppCore.Interfaces
{
    // Interfejs repozytorium przeznaczony dla encji Company.
    // Dziedziczy po generycznym repozytorium asynchronicznym, więc automatycznie ma podstawowe operacje CRUD:
    // - FindByIdAsync
    // - FindAllAsync
    // - FindPagedAsync
    // - AddAsync
    // - UpdateAsync
    // - RemoveByIdAsync
    public interface ICompanyRepository : IGenericRepositoryAsync<Company>
    {
        // Wyszukuje firmy na podstawie fragmentu nazwy.
        // nameQuery może być np. "tech", "soft", "pol".
        // Zwraca wszystkie firmy, których nazwa pasuje do zapytania.
        Task<IEnumerable<Company>> FindByNameAsync(string nameQuery);

        // Wyszukuje firmę po numerze NIP.
        // Zwraca Company? — czyli firmę lub null, jeśli nie istnieje.
        // NIP jest unikalny, więc wynik to maksymalnie jeden rekord.
        Task<Company?> FindByNipAsync(string NIP);

        // Pobiera listę pracowników firmy o podanym identyfikatorze.
        // Relacja wynika z modelu:
        // Person → Company (Employer)
        // Zwraca wszystkie osoby, które mają ustawione Employer.Id == companyId.
        Task<IEnumerable<Person>> GetEmployeesAsync(Guid companyId);
    }
}
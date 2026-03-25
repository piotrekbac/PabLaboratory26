using System.Collections.Generic; // Wymagane dla List<T>

// Piotr Bacior - WSEI Kraków

namespace AppCore.DTOs
{
    // Generyczny rekord reprezentujący wynik stronicowany.
    // Używany np. przy zwracaniu list kontaktów, firm, osób itp.
    public record PagedResult<T>(
        List<T> Items,   // Elementy na bieżącej stronie
        int TotalCount,  // Łączna liczba elementów
        int Page,        // Numer strony
        int PageSize     // Liczba elementów na stronie
    )
    {
        // Łączna liczba stron (zaokrąglona w górę)
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

        // Czy istnieje następna strona?
        public bool HasNext => Page < TotalPages;

        // Czy istnieje poprzednia strona? 
        public bool HasPrevious => Page > 1;
    }
}
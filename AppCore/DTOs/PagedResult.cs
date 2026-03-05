using System.Collections.Generic; // Kluczowe — bez tego List<T> nie byłby widoczny

namespace AppCore.DTOs
{
    // Rekord generyczny reprezentujący wynik stronicowany (paged result).
    // Używany np. przy zwracaniu list kontaktów, firm, osób itp.
    public record PagedResult<T>(
        List<T> Items,   // Lista elementów na danej stronie
        int TotalCount,  // Łączna liczba wszystkich elementów (bez paginacji)
        int Page,        // Numer bieżącej strony (1-based)
        int PageSize     // Liczba elementów na stronie
    )
    {
        // Liczba wszystkich stron — zaokrąglona w górę.
        // Przykład: TotalCount = 23, PageSize = 10 → TotalPages = 3
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

        // Czy istnieje następna strona?
        // True, jeśli bieżąca strona jest mniejsza niż liczba stron.
        public bool HasNext => Page < TotalPages;

        // Czy istnieje poprzednia strona?
        // True, jeśli bieżąca strona jest większa niż 1.
        public bool HasPrevious => Page > 1;
    }
}
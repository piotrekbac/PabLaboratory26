using AppCore.Models.Enums;     

namespace AppCore.Models 
{
    // Klasa Company dziedziczy po klasie Contact
    public class Company : Contact
    {
        // Nazwa firmy – wymagane pole (nie może być null, ale może być pustym stringiem)
        public string Name { get; set; } = string.Empty;

        // NIP firmy – może być null (np. gdy nie jest podany)
        public string? NIP { get; set; }

        // REGON firmy – również opcjonalny
        public string? REGON { get; set; }

        // Numer KRS – opcjonalny
        public string? KRS { get; set; }

        // Branża, w której działa firma – opcjonalna informacja
        public string? Industry { get; set; }

        // Liczba pracowników – opcjonalna (int? oznacza typ nullable)
        public int? EmployeeCount { get; set; }

        // Roczny przychód – opcjonalny, decimal? bo to wartość finansowa
        public decimal? AnnualRevenue { get; set; }

        // Strona internetowa firmy – opcjonalna
        public string? Website { get; set; }

        // Lista pracowników powiązanych z firmą.
        // Inicjalizowana pustą listą, żeby nie była nullem.
        public List<Person> Employees { get; set; } = new();

        // Główna osoba kontaktowa w firmie – może być null, jeśli nie ustawiono
        public Person? PrimaryContact { get; set; }

        // Nadpisanie metody z klasy bazowej Contact -> Zwraca nazwę firmy jako "wyświetlaną nazwę".
        public override string GetDisplayName() => Name;
    }
}
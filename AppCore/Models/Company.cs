using AppCore.DTOs;
using AppCore.Models.Enums;     

// Piotr Bacior - WSEI Kraków

namespace AppCore.Models 
{
    // Encja Company dziedziczy po Contact — ma Email, Phone, Address, Tags, Notes itd.
    public class Company : Contact
    {
        public string Name { get; set; } = string.Empty; // nazwa firmy
        public string? NIP { get; set; }                 // opcjonalny NIP
        public string? REGON { get; set; }               // opcjonalny REGON
        public string? KRS { get; set; }                 // opcjonalny KRS
        public string? Industry { get; set; }            // branża
        public int? EmployeeCount { get; set; }          // liczba pracowników
        public decimal? AnnualRevenue { get; set; }      // przychód roczny
        public string? Website { get; set; }             // strona www

        // Lista pracowników powiązanych z firmą
        public List<Person> Employees { get; set; } = new();

        // Główna osoba kontaktowa
        public Person? PrimaryContact { get; set; }

        // Zwraca nazwę firmy jako nazwę wyświetlaną
        public override string GetDisplayName() => Name;

        // Konwersja encji → DTO
        public CompanyDto ToDto() => new()
        {
            Id = this.Id,
            Name = this.Name,
            NIP = this.NIP,
            Industry = this.Industry,
            Website = this.Website
        };
    }
}
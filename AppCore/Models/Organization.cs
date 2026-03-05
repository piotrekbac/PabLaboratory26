using AppCore.Models.Enums;     // Import enumów, m.in. OrganizationType

namespace AppCore.Models
{
    // Organization dziedziczy po Contact, więc ma Email, Phone, Address, Status, Tags, Notes itd.
    public class Organization : Contact
    {
        // Nazwa organizacji — wymagane pole (string bez '?')
        public string Name { get; set; }

        // Typ organizacji — enum (np. NGO, Foundation, Association)
        public OrganizationType Type { get; set; }

        // Numer KRS — opcjonalny
        public string? KRS { get; set; }

        // Strona internetowa — opcjonalna
        public string? Website { get; set; }

        // Misja organizacji — opcjonalna
        public string? Mission { get; set; }

        // Lista członków organizacji — brak inicjalizacji → może być null
        public List<Person> Members { get; set; }

        // Główna osoba kontaktowa — opcjonalna
        public Person? PrimaryContact { get; set; }
        
        // Nadpisanie metody z Contact — zwraca nazwę organizacji
        public override string GetDisplayName() => Name;
    }
}
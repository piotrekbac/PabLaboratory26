using AppCore.Models.Enums;     // Import enumów, m.in. OrganizationType

// Piotr Bacior - WSEI Kraków

namespace AppCore.Models
{
    // Encja Organization dziedziczy po Contact.
    public class Organization : Contact
    {
        public string Name { get; set; }                 // nazwa organizacji
        public OrganizationType Type { get; set; }       // typ organizacji
        public string? KRS { get; set; }                 // opcjonalny KRS
        public string? Website { get; set; }             // strona www
        public string? Mission { get; set; }             // misja organizacji

        // Lista członków — brak inicjalizacji → może być null
        public List<Person> Members { get; set; }

        public Person? PrimaryContact { get; set; }      // główny kontakt

        public override string GetDisplayName() => Name; // nazwa wyświetlana
    }
}
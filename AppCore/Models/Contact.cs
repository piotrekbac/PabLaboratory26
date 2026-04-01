using AppCore.Models.Enums; // Import enumów, m.in. ContactStatus

// Piotr Bacior - WSEI Kraków

namespace AppCore.Models
{
    // Abstrakcyjna klasa bazowa dla Person, Company, Organization.
    public abstract class Contact : EntityBase
    {
        public string Email { get; set; }           // email kontaktu
        public string Phone { get; set; }           // numer telefonu
        public Address? Address { get; set; }       // adres kontaktu

        public DateTime CreatedAt { get; set; }     // data utworzenia
        
        public DateTime? UpdatedAt { get; set; }    // data aktualizacji
        public ContactStatus Status { get; set; }   // status kontaktu

        public List<Tag> Tags { get; set; }         // lista tagów

        // Lista notatek — brak inicjalizacji → wymaga ustawienia w konstruktorze lub serwisie
        public List<Note> Notes;

        // Każda klasa dziedzicząca musi zwrócić nazwę wyświetlaną
        public abstract string GetDisplayName();
    }
}
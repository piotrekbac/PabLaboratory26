using Microsoft.VisualBasic;

// Piotr Bacior - WSEI Kraków

namespace AppCore.Models
{
    // Klasa Note dziedziczy po EntityBase, czyli ma wspólne pola encji (np. Id)
    public class Note : EntityBase
    {
        // Treść notatki — wymagane pole (string bez '?')
        public string Content { get; set; } 

        // Data utworzenia notatki
        public DateTime CreatedAt { get; set; } = DateAndTime.Now;

        // Informacja o autorze notatki — np. nazwa użytkownika
        public string CreatedBy { get; set; }
    }
}
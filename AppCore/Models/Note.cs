namespace AppCore.Models
{
    // Klasa Note dziedziczy po EntityBase, czyli ma wspólne pola encji (np. Id)
    public class Note : EntityBase
    {
        // Dodatkowy identyfikator notatki — uwaga: dubluje Id z EntityBase
        public Guid id { get; set; }

        // Treść notatki — wymagane pole (string bez '?')
        public string Content { get; set; }

        // Data utworzenia notatki
        public DateTime CreatedAt { get; set; }

        // Informacja o autorze notatki — np. nazwa użytkownika
        public string CreatedBy { get; set; }
    }
}
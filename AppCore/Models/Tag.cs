// Piotr Bacior - WSEI Kraków

namespace AppCore.Models
{
    // Klasa Tag reprezentuje etykietę przypisaną do kontaktu.
    // Dziedziczy po EntityBase, więc ma automatycznie generowane Id.
    public class Tag : EntityBase
    {
        // Dodatkowy identyfikator — UWAGA: duplikuje Id z EntityBase.
        // W praktyce nie powinno się dublować kluczy
        //public Guid id { get; set; }

        // Nazwa tagu — np. "VIP", "Lead", "Partner".
        public string Name { get; set; } = string.Empty;

        // Kolor tagu — np. "#FF0000" lub "red".
        public string Color { get; set; } = string.Empty;
    }
}
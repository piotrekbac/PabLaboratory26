using Microsoft.VisualBasic;

// Piotr Bacior - WSEI Kraków

namespace AppCore.Models
{
    // Model notatki przypisanej do kontaktu.
    public class Note : EntityBase
    {
        public string Content { get; set; } // treść notatki
        public DateTime CreatedAt { get; set; } = DateAndTime.Now; // data utworzenia
        public string CreatedBy { get; set; } // autor notatki
    }
}
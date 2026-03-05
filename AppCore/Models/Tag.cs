namespace AppCore.Models
{
    // Tag dziedziczy po EntityBase, więc ma już podstawowe pola encji (np. Id)
    public class Tag : EntityBase
    {
        // Dodatkowy identyfikator — uwaga: dubluje Id z EntityBase
        public Guid id { get; set; }

        // Nazwa tagu — wymagane pole (np. "VIP", "Lead", "Partner")
        public string Name { get; set; }

        // Kolor tagu — wymagane pole (np. "#FF0000" lub "red")
        public string Color { get; set; }
    }
}
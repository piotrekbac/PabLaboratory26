using AppCore.Models.Enums;     // Import enumów, m.in. AddressType

// Piotr Bacior - WSEI Kraków

namespace AppCore.Models
{
    // Model domenowy reprezentujący adres kontaktu.
    // Dziedziczy po EntityBase (zawiera m.in. Id).
    public class Address : EntityBase
    {
        // Ulica i numer — wymagane
        public string Street { get; set; }

        // Miasto — wymagane
        public string City { get; set; }

        // Kod pocztowy — wymagane
        public string PostalCode { get; set; }

        // Kraj — wymagane
        public string Country { get; set; }

        // Typ adresu — enum (Home, Office itd.)
        public AddressType Type { get; set; }
    }
}
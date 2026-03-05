using AppCore.Models.Enums;     // Import enumów, m.in. AddressType

namespace AppCore.Models        // Przestrzeń nazw dla modeli domenowych
{
    // Klasa Address dziedziczy po EntityBase (np. Id, daty utworzenia, itp.)
    public class Address : EntityBase
    {
        // Ulica i numer – wymagane, bo typ string bez '?' (nie jest to pole typu: nullable)
        public string Street { get; set; }

        // Miasto – również wymagane
        public string City { get; set; }

        // Kod pocztowy – wymagany
        public string PostalCode { get; set; }

        // Kraj – wymagany
        public string Country { get; set; }

        // Typ adresu – enum (np. Home, Office, Billing, Shipping)
        public AddressType Type { get; set; }
    }
}
namespace AppCore.Exceptions;

// Piotr Bacior - WSEI Kraków

// Niestandardowy wyjątek rzucany, gdy kontakt o podanym ID nie istnieje.
// Dziedziczy po Exception i przyjmuje komunikat błędu w konstruktorze.
public class ContactNotFoundException : Exception
{
    public ContactNotFoundException(string msg) : base(msg) { }
}
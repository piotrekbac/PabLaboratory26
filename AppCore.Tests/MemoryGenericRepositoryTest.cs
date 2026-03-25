using AppCore.Models;
using Interfaces.Memory;
using Xunit;

// Piotr Bacior - WSEI Kraków

namespace UnitTests;

// Testy wykonane w metodyce AAA (Arrange, Act, Assert)
public class MemoryGenericRepositoryTest
{
    // Tworzymy repozytorium w pamięci dla encji Person
    private readonly MemoryGenericRepository<Person> _repo = new();

    // Test odpowiedzialny za dodawanie i pobieranie osoby 
    [Fact]
    public async Task AddAndFindPersonTestAsync()
    {
        // Arrange — przygotowanie danych
        var person = new Person { FirstName = "Adam", LastName = "Kowalski" };
        
        // Act — wykonanie operacji
        await _repo.AddAsync(person);
        var actual = await _repo.FindByIdAsync(person.Id);
        
        // Assert — weryfikacja wyniku
        Assert.NotNull(actual);
        Assert.Equal(person.Id, actual?.Id);
        Assert.Equal("Adam", actual?.FirstName);
    }

    // Test odpowiedzialny za aktualizację istniejącej osoby
    [Fact]
    public async Task UpdatePersonTestAsync()
    {
        // Arrange
        var person = new Person { FirstName = "Adam" };
        await _repo.AddAsync(person);
        
        // Act — zmieniamy imię i aktualizujemy encję
        person.FirstName = "Ewa";
        await _repo.UpdateAsync(person);
        var actual = await _repo.FindByIdAsync(person.Id);
      
        // Assert
        Assert.Equal("Ewa", actual?.FirstName);
    }

    // Test odpowiedzialny za usuwanie istniejącej osoby
    [Fact]
    public async Task RemovePersonTestAsync()
    {
        // Arrange
        var person = new Person();
        await _repo.AddAsync(person);

        // Act — usuwamy osobę
        await _repo.RemoveByIdAsync(person.Id);
        var actual = await _repo.FindByIdAsync(person.Id);

        // Assert — osoba powinna być null
        Assert.Null(actual);
    }

    // Test odpowiedzialny za pobieranie osób stronicowanych
    [Fact]
    public async Task FindPagedTestAsync()
    {
        // Arrange — dodajemy 5 osób
        for (int i = 0; i < 5; i++)
        {
            await _repo.AddAsync(new Person { FirstName = $"Osoba {i}" });
        }

        // Act — pobieramy pierwszą stronę po 2 elementy
        var page1 = await _repo.FindPagedAsync(1, 2);

        // Assert
        Assert.Equal(2, page1.Items.Count); // 2 osoby na stronie
        Assert.Equal(5, page1.TotalCount);  // łącznie 5 osób
        Assert.Equal(3, page1.TotalPages);  // 3 strony (2 + 2 + 1)
    }
}

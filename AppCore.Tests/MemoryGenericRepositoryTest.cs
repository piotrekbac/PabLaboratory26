using AppCore.Models;
using Interfaces.Memory;
using Xunit;

namespace UnitTests;

public class MemoryGenericRepositoryTest
{
    private readonly MemoryGenericRepository<Person> _repo = new();

    [Fact]
    public async Task AddAndFindPersonTestAsync()
    {
        var person = new Person { FirstName = "Adam", LastName = "Kowalski" };
        
        await _repo.AddAsync(person);
        var actual = await _repo.FindByIdAsync(person.Id);
        
        Assert.NotNull(actual);
        Assert.Equal(person.Id, actual?.Id);
        Assert.Equal("Adam", actual?.FirstName);
    }

    [Fact]
    public async Task UpdatePersonTestAsync()
    {
        var person = new Person { FirstName = "Adam" };
        await _repo.AddAsync(person);
        
        person.FirstName = "Ewa";
        await _repo.UpdateAsync(person);
        var actual = await _repo.FindByIdAsync(person.Id);
      
        Assert.Equal("Ewa", actual?.FirstName);
    }

    [Fact]
    public async Task RemovePersonTestAsync()
    {
        // Arrange
        var person = new Person();
        await _repo.AddAsync(person);

        // Act
        await _repo.RemoveByIdAsync(person.Id);
        var actual = await _repo.FindByIdAsync(person.Id);

        // Assert
        Assert.Null(actual);
    }

    [Fact]
    public async Task FindPagedTestAsync()
    {
        // Arrange
        for (int i = 0; i < 5; i++)
        {
            await _repo.AddAsync(new Person { FirstName = $"Osoba {i}" });
        }

        // Act
        var page1 = await _repo.FindPagedAsync(1, 2); // Pobieramy 2 osoby z 5

        // Assert
        Assert.Equal(2, page1.Items.Count);
        Assert.Equal(5, page1.TotalCount);
        Assert.Equal(3, page1.TotalPages); // 5 / 2 = 3 strony
    }
}
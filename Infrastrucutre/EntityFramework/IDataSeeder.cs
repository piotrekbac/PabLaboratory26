namespace Infrastructure.EntityFramework;

public interface IDataSeeder
{
    public int Order { get; }
    Task SeedAsync();
}
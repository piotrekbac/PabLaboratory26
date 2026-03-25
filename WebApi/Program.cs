using AppCore.Interfaces;
using AppCore.Module;
using AppCore.Services;
using Infrastrucutre.Memory;
using Interfaces.Memory; 

// Piotr Bacior - WSEI Kraków

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();  // <-- TO JEST KLUCZOWE!
        builder.Services.AddAuthorization();

        // --- Rejestracja serwisów w kontenerze Dependency Injection (DI) ---
        // Kontener zarządza cyklem życia obiektów. Singleton oznacza jedną instancję dla całej aplikacji.
        
        builder.Services.AddAuthorization();        // dodaje

        builder.Services.AddOpenApi();
            
        // Stare repozytorium (z lab 2)
        builder.Services.AddSingleton<ICustomerService, MemoryCustomerService1>();

        // 1. Rejestracja konkretnego repozytorium dla osób (Person)
        // Kiedy serwis poprosi o IPersonRepository, dostanie MemoryPersonRepository.
        builder.Services.AddSingleton<IPersonRepository, MemoryPersonRepository>();
        builder.Services.AddSingleton<ICompanyRepository, MemoryCompanyRepository>();
        builder.Services.AddSingleton<IOrganizationRepository, MemoryOrganizationRepository>();
        builder.Services.AddExceptionHandler<WebApi.Middleware.ProblemDetailsExceptionHandler>();
        builder.Services.AddProblemDetails();
        
        // 2. Rejestracja UnitOfWork
        // UnitOfWork grupuje repozytoria. Używamy fabryki (sp => ...), aby wstrzyknąć 
        // wcześniej zarejestrowane repozytoria do jego konstruktora.
        builder.Services.AddSingleton<IContactUnitOfWork>(sp =>
        {
            var persons = sp.GetRequiredService<IPersonRepository>();
            var companies = sp.GetRequiredService<ICompanyRepository>();
            var orgs = sp.GetRequiredService<IOrganizationRepository>(); // To musi zadziałać
            return new MemoryContactUnitOfWork(persons, companies, orgs);
        });

        // 3. Rejestracja głównego serwisu biznesowego
        // Serwis wymaga UnitOfWork, który został zarejestrowany powyżej.
        builder.Services.AddSingleton<IPersonService, MemoryPersonService>();
        
        // Rejestracja AddContactModule 
        builder.Services.AddContactsModule(builder.Configuration);
        
        
        builder.Services.AddOpenApi();
        
        var app = builder.Build();

        // --- Konfiguracja potoku przetwarzania żądań HTTP ---
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        //app.UseHttpsRedirection();
        app.UseAuthorization();
        
        // --- Endpoints ---
        
        // Endpoint dla starych klientów
        app.MapGet("/api/customers", (ICustomerService service) =>
            {
                return service.GetCustomers();
            })
            .WithName("GetCustomers");

        // --- Automatyczne mapowanie kontrolerów ---
        // Dzięki temu kontroler ContactsController zostanie automatycznie wykryty przez API.
        app.MapControllers(); 

        
        app.UseExceptionHandler();
        app.MapControllers();
        
        app.Run();
    }
}
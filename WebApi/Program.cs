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

        // Rejestracja kontrolerów — kluczowe dla działania API
        builder.Services.AddControllers();
        builder.Services.AddAuthorization();

        // Rejestracja OpenAPI (Swagger)
        builder.Services.AddOpenApi();

        // Stare repozytorium z lab 2 — pozostawione poglądowo
        builder.Services.AddSingleton<ICustomerService, MemoryCustomerService1>();

        // Rejestracja repozytoriów pamięciowych
        builder.Services.AddSingleton<IPersonRepository, MemoryPersonRepository>();
        builder.Services.AddSingleton<ICompanyRepository, MemoryCompanyRepository>();
        builder.Services.AddSingleton<IOrganizationRepository, MemoryOrganizationRepository>();

        // Rejestracja globalnego handlera wyjątków
        builder.Services.AddExceptionHandler<WebApi.Middleware.ProblemDetailsExceptionHandler>();
        builder.Services.AddProblemDetails();
        
        // Rejestracja UnitOfWork — łączy repozytoria w jedną transakcję
        builder.Services.AddSingleton<IContactUnitOfWork>(sp =>
        {
            var persons = sp.GetRequiredService<IPersonRepository>();
            var companies = sp.GetRequiredService<ICompanyRepository>();
            var orgs = sp.GetRequiredService<IOrganizationRepository>();
            return new MemoryContactUnitOfWork(persons, companies, orgs);
        });

        // Rejestracja głównego serwisu biznesowego
        builder.Services.AddSingleton<IPersonService, MemoryPersonService>();
        
        // Rejestracja modułu kontaktów (AutoMapper + walidatory)
        builder.Services.AddContactsModule(builder.Configuration);
        
        var app = builder.Build();

        // Swagger tylko w trybie developerskim
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseAuthorization();

        // Endpoint z lab 2 — pozostawiony poglądowo
        app.MapGet("/api/customers", (ICustomerService service) =>
            service.GetCustomers()
        ).WithName("GetCustomers");

        // Automatyczne mapowanie kontrolerów
        app.MapControllers();

        // Globalny handler wyjątków
        app.UseExceptionHandler();

        app.Run();
    }
}

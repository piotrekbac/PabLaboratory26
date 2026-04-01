using AppCore.Interfaces;
using AppCore.Module;
using AppCore.Services;
using Infrastructure.EntityFramework.Context;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using WebApi.Middleware;

// Piotr Bacior - WSEI Kraków

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Rejestracja MVC/API
        builder.Services.AddControllers();
        builder.Services.AddAuthorization();
        builder.Services.AddOpenApi();

        // Rejestracja Modułu (Walidatory i AutoMapper)
        builder.Services.AddContactsModule(builder.Configuration);

        // Konfiguracja Bazy Danych (Entity Framework Core)
        // Pobieramy ConnectionString z appsettings.json
        builder.Services.AddDbContext<ContactsDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("CrmDb")));

        // Rejestracja Repozytoriów (Entity Framework)
        builder.Services.AddScoped<IPersonRepository, EfPersonRepository>();
        builder.Services.AddScoped<ICompanyRepository, EfCompanyRepository>();
        builder.Services.AddScoped<IOrganizationRepository, EfOrganizationRepository>();

        // Rejestracja UnitOfWork (Entity Framework)
        builder.Services.AddScoped<IContactUnitOfWork, EfContactsUnitOfWork>();

        // Rejestracja Serwisów
        builder.Services.AddScoped<IPersonService, MemoryPersonService>();

        // Obsługa wyjątków
        builder.Services.AddExceptionHandler<ProblemDetailsExceptionHandler>();
        builder.Services.AddProblemDetails();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseExceptionHandler(); // Musi być przed routingiem/kontrolerami
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
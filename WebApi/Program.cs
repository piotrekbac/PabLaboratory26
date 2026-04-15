using AppCore.Interfaces;
using AppCore.Module;
using AppCore.Services;
using Infrastructure.EntityFramework; 
using Infrastructure.EntityFramework.Context;
using Infrastructure.Security; 
using Microsoft.EntityFrameworkCore;
using WebApi.Middleware;

// Piotr Bacior - WSEI Kraków

namespace WebApi;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // 1. Rejestracja MVC/API
        builder.Services.AddControllers();
        builder.Services.AddAuthorization();
        builder.Services.AddOpenApi();

        // 2. Rejestracja ustawień JWT (musi być przed AddJwt)
        builder.Services.AddSingleton<JwtSettings>();

        // 3. Moduły (Walidatory, Automapper, EF Core, Identity, JWT, Serwisy)
        builder.Services.AddContactsModule(builder.Configuration);
        builder.Services.AddContactsEfModule(builder.Configuration); 
        builder.Services.AddJwt(new JwtSettings(builder.Configuration));

        // 4. Inne serwisy
        builder.Services.AddScoped<IPersonService, MemoryPersonService>();
        builder.Services.AddScoped<IDataSeeder, IdentityDbSeeder>(); 

        // 5. Obsługa wyjątków
        builder.Services.AddExceptionHandler<ProblemDetailsExceptionHandler>();
        builder.Services.AddProblemDetails();

        var app = builder.Build();

        // 6. Inicjalizacja bazy i Seedowanie danych przy starcie
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ContactsDbContext>();
            await db.Database.MigrateAsync();   // Automatyczna migracja (tworzy plik .db jeśli go nie ma)

            var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
            await seeder.SeedAsync();           // Seedowanie ról i użytkowników
        }

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseExceptionHandler();  // Musi być przed routingiem/kontrolerami
        app.UseAuthentication();    // Autoryzacja przez JWT
        app.UseAuthorization();     // Sprawdzanie polityk (AdminOnly, itp.)
        app.MapControllers();

        await app.RunAsync();
    }
}
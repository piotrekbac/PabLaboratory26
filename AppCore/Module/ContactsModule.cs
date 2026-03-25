using AppCore.Mapper; 
using AppCore.Validators; 
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// Piotr Bacior - WSEI Kraków

namespace AppCore.Module;

public static class ContactsModule
{
    // Metoda rozszerzająca — pozwala dodać moduł kontaktów do DI.
    public static IServiceCollection AddContactsModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // 1. Rejestracja walidatorów FluentValidation
        // Automatycznie znajdzie wszystkie klasy Validator w tym assembly.
        services.AddValidatorsFromAssemblyContaining<CreatePersonDtoValidator>();
        
        // 2. Rejestracja AutoMappera
        // AutoMapper przeskanuje assembly i znajdzie ContactsMappingProfile.
        services.AddAutoMapper(typeof(ContactsMappingProfile).Assembly);
        
        return services;
    }
}
using AppCore.Mapper; 
using AppCore.Validators; 
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppCore.Module;

public static class ContactsModule
{
    public static IServiceCollection AddContactsModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // 1. Rejestracja walidatorów (FluentValidation)
        services.AddValidatorsFromAssemblyContaining<CreatePersonDtoValidator>();
        
        // 2. Rejestracja AutoMappera
        // Automapper przeszuka Assembly, w którym znajduje się profil i zarejestruje go
        services.AddAutoMapper(typeof(ContactsMappingProfile).Assembly);
        
        return services;
    }
}
using AutoMapper;
using AppCore.Models;
using AppCore.DTOs;

// Piotr Bacior - WSEI Kraków

namespace AppCore.Mapper;

// Profil mapowania AutoMapper — definiuje konwersje między encjami a DTO
public class ContactsMappingProfile : Profile
{
    public ContactsMappingProfile()
    {
        // Mapowanie encji Person -> PersonDto
        // Przykładowe użycie ForMember — pokazuje składnię, choć AutoMapper zrobiłby to automatycznie
        CreateMap<Person, PersonDto>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));

        // Mapowanie DTO -> encja (używane przy tworzeniu i aktualizacji osoby)
        CreateMap<CreatePersonDto, Person>();
        CreateMap<UpdatePersonDto, Person>();

        // Mapowanie adresu w obie strony (Address <-> AddressDto)
        CreateMap<Address, AddressDto>().ReverseMap();

        // Mapowanie notatek
        CreateMap<Note, NoteDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) // jawne mapowanie ID
            .ReverseMap();
    }
}
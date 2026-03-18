using AutoMapper;
using AppCore.Models;
using AppCore.DTOs;

namespace AppCore.Mapper;

public class ContactsMappingProfile : Profile
{
    public ContactsMappingProfile()
    {
        // Definicja mapowania: z encji Person na PersonDto
        CreateMap<Person, PersonDto>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName)) // Automatyczne, ale pokazuję składnię
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));
        
        // Mapowanie w drugą stronę: z DTO na encję (używane przy POST/PUT)
        CreateMap<CreatePersonDto, Person>();
        CreateMap<UpdatePersonDto, Person>();
        
        // Mapowanie adresu
        CreateMap<Address, AddressDto>().ReverseMap();
    }
}
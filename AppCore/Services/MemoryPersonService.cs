using AppCore.DTOs;
using AppCore.Interfaces;
using AppCore.Models;
using AutoMapper;

namespace AppCore.Services;

public class MemoryPersonService(IContactUnitOfWork unitOfWork, IMapper mapper) : IPersonService
{
    public async Task<PagedResult<PersonDto>> FindAllPeoplePaged(int page, int size)
    {
        var result = await unitOfWork.Persons.FindPagedAsync(page, size);
        var items = mapper.Map<List<PersonDto>>(result.Items);
        return new PagedResult<PersonDto>(items, result.TotalCount, result.Page, result.PageSize);
    }

    public async Task<IEnumerable<PersonDto>> FindPeopleFromCompany(Guid companyId)
    {
        var persons = await unitOfWork.Persons.GetEmployeesByCompanyAsync(companyId);
        return mapper.Map<IEnumerable<PersonDto>>(persons);
    }

    public async Task<PersonDto> GetById(Guid id)
    {
        var person = await unitOfWork.Persons.FindByIdAsync(id);
        if (person == null) throw new KeyNotFoundException("Osoba nie istnieje.");
        return mapper.Map<PersonDto>(person);
    }

    public async Task<PersonDto> CreatePerson(CreatePersonDto personDto)
    {
        var entity = mapper.Map<Person>(personDto);
        await unitOfWork.Persons.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return mapper.Map<PersonDto>(entity);
    }

    public async Task<PersonDto> UpdatePerson(Guid id, UpdatePersonDto personDto)
    {
        var entity = await unitOfWork.Persons.FindByIdAsync(id);
        if (entity == null) throw new KeyNotFoundException("Osoba nie istnieje.");

        // Mapujemy zmiany z DTO na istniejącą encję
        mapper.Map(personDto, entity);
        
        await unitOfWork.Persons.UpdateAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return mapper.Map<PersonDto>(entity);
    }

    public async Task DeletePerson(Guid id)
    {
        await unitOfWork.Persons.RemoveByIdAsync(id);
        await unitOfWork.SaveChangesAsync();
    }
}
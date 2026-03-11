using AppCore.DTOs;
using AppCore.Interfaces;
using AppCore.Models;

namespace AppCore.Services;

public class MemoryPersonService(IContactUnitOfWork unitOfWork) : IPersonService
{
    public async Task<PagedResult<PersonDto>> FindAllPeoplePaged(int page, int size)
    {
        var pagedPersons = await unitOfWork.Persons.FindPagedAsync(page, size);
        
        // Mapowanie encji na DTO
        var items = pagedPersons.Items.Select(p => p.ToDto()).ToList();
        
        return new PagedResult<PersonDto>(items, pagedPersons.TotalCount, pagedPersons.Page, pagedPersons.PageSize);
    }

    public async Task<IEnumerable<PersonDto>> FindPeopleFromCompany(Guid companyId)
    {
        var persons = await unitOfWork.Persons.GetEmployeesByCompanyAsync(companyId);
        return persons.Select(p => p.ToDto());
    }

    public async Task<PersonDto> CreatePerson(CreatePersonDto dto)
    {
        var person = dto.ToEntity();
        var created = await unitOfWork.Persons.AddAsync(person);
        await unitOfWork.SaveChangesAsync();
        return created.ToDto();
    }

    public async Task<PersonDto?> GetById(Guid id)
    {
        var person = await unitOfWork.Persons.FindByIdAsync(id);
        return person?.ToDto();
    }

    public async Task UpdatePerson(Guid id, UpdatePersonDto dto)
    {
        var person = await unitOfWork.Persons.FindByIdAsync(id);
        if (person == null) throw new KeyNotFoundException("Osoba nie istnieje");
        
        // Tu aktualizujemy właściwości (uproszczone)
        person.FirstName = dto.FirstName ?? person.FirstName;
        person.LastName = dto.LastName ?? person.LastName;
        
        await unitOfWork.Persons.UpdateAsync(person);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeletePerson(Guid id)
    {
        await unitOfWork.Persons.RemoveByIdAsync(id);
        await unitOfWork.SaveChangesAsync();
    }
}
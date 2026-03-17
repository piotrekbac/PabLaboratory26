using AppCore.DTOs;
using AppCore.Interfaces;
using AppCore.Models;

namespace AppCore.Services;

public class MemoryPersonService(IContactUnitOfWork unitOfWork) : IPersonService
{
    public async Task<PagedResult<PersonDto>> FindAllPeoplePaged(int page, int size)
    {
        var paged = await unitOfWork.Persons.FindPagedAsync(page, size);
        return new PagedResult<PersonDto>(
            paged.Items.Select(p => p.ToDto()).ToList(), 
            paged.TotalCount, 
            paged.Page, 
            paged.PageSize
        );
    }

    public async Task<PersonDto> CreatePerson(CreatePersonDto dto)
    {
        var person = dto.ToEntity();
        await unitOfWork.Persons.AddAsync(person);
        await unitOfWork.SaveChangesAsync();
        return person.ToDto();
    }

    public async Task UpdatePerson(Guid id, UpdatePersonDto dto)
    {
        var person = await unitOfWork.Persons.FindByIdAsync(id);
        if (person == null) throw new KeyNotFoundException("Nie znaleziono osoby");

        if (dto.FirstName != null) person.FirstName = dto.FirstName;
        if (dto.LastName != null) person.LastName = dto.LastName;
        if (dto.Email != null) person.Email = dto.Email;
        
        await unitOfWork.Persons.UpdateAsync(person);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeletePerson(Guid id)
    {
        await unitOfWork.Persons.RemoveByIdAsync(id);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<PersonDto?> GetById(Guid id)
    {
        var person = await unitOfWork.Persons.FindByIdAsync(id);
        return person?.ToDto();
    }

    public Task<IEnumerable<PersonDto>> FindPeopleFromCompany(Guid companyId) => throw new NotImplementedException();
}
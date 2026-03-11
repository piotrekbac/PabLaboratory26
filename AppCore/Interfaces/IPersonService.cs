using AppCore.DTOs;
using AppCore.Models;

namespace AppCore.Interfaces;

public interface IPersonService
{
    Task<PagedResult<PersonDto>> FindAllPeoplePaged(int page, int size);
    Task<IEnumerable<PersonDto>> FindPeopleFromCompany(Guid companyId);
    Task<PersonDto?> GetById(Guid id);
    Task<PersonDto> CreatePerson(CreatePersonDto dto);
    Task UpdatePerson(Guid id, UpdatePersonDto dto);
    Task DeletePerson(Guid id);
}
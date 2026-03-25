using AppCore.DTOs;
using AppCore.Models;

// Piotr Bacior - WSEI Kraków

namespace AppCore.Interfaces;

public interface IPersonService
{
    Task<PagedResult<PersonDto>> FindAllPeoplePaged(int page, int size);
    Task<IEnumerable<PersonDto>> FindPeopleFromCompany(Guid companyId);
    Task<PersonDto> GetById(Guid id);
    Task<PersonDto> CreatePerson(CreatePersonDto personDto);
    Task<PersonDto> UpdatePerson(Guid id, UpdatePersonDto personDto);
    Task DeletePerson(Guid id);
    Task<NoteDto> AddNoteToPerson(Guid personId, CreateNoteDto noteDto);
    Task<PersonDto> GetPerson(Guid personId); 
    Task DeleteNoteFromPerson(Guid personId, Guid noteId);
}
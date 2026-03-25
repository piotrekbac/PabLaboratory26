using AppCore.DTOs;
using AppCore.Exceptions;
using AppCore.Interfaces;
using AppCore.Models;
using AutoMapper;

// Piotr Bacior - WSEI Kraków

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
    
    public async Task<NoteDto> AddNoteToPerson(Guid personId, CreateNoteDto noteDto)
    {
        var person = await unitOfWork.Persons.FindByIdAsync(personId);
        if (person == null) 
            throw new ContactNotFoundException($"Person with id={personId} not found!");

        person.Notes ??= new List<Note>();

        var note = new Note 
        { 
            Content = noteDto.Content, 
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "System" 
        };
    
        // Wymuszenie ID:
        note.Id = Guid.NewGuid(); 

        person.Notes.Add(note);
    
        await unitOfWork.Persons.UpdateAsync(person);
        await unitOfWork.SaveChangesAsync();

        // Sprawdź w debuggerze (postaw breakpoint tutaj): jakie ID ma 'note' przed zwróceniem?
        return mapper.Map<NoteDto>(note);
    }

    public async Task<PersonDto> GetPerson(Guid personId)
    {
        var person = await unitOfWork.Persons.FindByIdAsync(personId);
        if (person == null) throw new ContactNotFoundException($"Person with id={personId} not found!");
        return mapper.Map<PersonDto>(person);
    }

    public async Task DeleteNoteFromPerson(Guid personId, Guid noteId)
    {
        var person = await unitOfWork.Persons.FindByIdAsync(personId);
        if (person == null) 
            throw new ContactNotFoundException($"Person with id={personId} not found!");
    
        // Sprawdź czy Notes nie jest nullem
        if (person.Notes == null) return; 

        // Znajdź notatkę
        var note = person.Notes.FirstOrDefault(n => n.Id == noteId);
    
        if (note != null)
        {
            person.Notes.Remove(note);
            await unitOfWork.Persons.UpdateAsync(person);
            await unitOfWork.SaveChangesAsync();
        }
        else 
        {
            throw new KeyNotFoundException("Notatka nie istnieje");
        }
    }
}
using AppCore.DTOs;
using AppCore.Exceptions;
using AppCore.Interfaces;
using AppCore.Models;
using AutoMapper;

// Piotr Bacior - WSEI Kraków

namespace AppCore.Services;

// Serwis działający w pamięci — implementacja IPersonService.
// Używany np. do testów lub prototypowania.
public class MemoryPersonService(IContactUnitOfWork unitOfWork, IMapper mapper) : IPersonService
{
    // Zwraca osoby w formie stronicowanej.
    public async Task<PagedResult<PersonDto>> FindAllPeoplePaged(int page, int size)
    {
        var result = await unitOfWork.Persons.FindPagedAsync(page, size);
        var items = mapper.Map<List<PersonDto>>(result.Items);
        return new PagedResult<PersonDto>(items, result.TotalCount, result.Page, result.PageSize);
    }

    // Zwraca osoby zatrudnione w firmie.
    public async Task<IEnumerable<PersonDto>> FindPeopleFromCompany(Guid companyId)
    {
        var persons = await unitOfWork.Persons.GetEmployeesByCompanyAsync(companyId);
        return mapper.Map<IEnumerable<PersonDto>>(persons);
    }

    // Pobiera osobę po ID.
    public async Task<PersonDto> GetById(Guid id)
    {
        var person = await unitOfWork.Persons.FindByIdAsync(id);
        if (person == null) throw new KeyNotFoundException("Osoba nie istnieje.");
        return mapper.Map<PersonDto>(person);
    }

    // Tworzy nową osobę.
    public async Task<PersonDto> CreatePerson(CreatePersonDto personDto)
    {
        var entity = mapper.Map<Person>(personDto);
        await unitOfWork.Persons.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return mapper.Map<PersonDto>(entity);
    }

    // Aktualizuje istniejącą osobę.
    public async Task<PersonDto> UpdatePerson(Guid id, UpdatePersonDto personDto)
    {
        var entity = await unitOfWork.Persons.FindByIdAsync(id);
        if (entity == null) throw new KeyNotFoundException("Osoba nie istnieje.");

        // Mapowanie zmian z DTO na encję.
        mapper.Map(personDto, entity);
        
        await unitOfWork.Persons.UpdateAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return mapper.Map<PersonDto>(entity);
    }

    // Usuwa osobę.
    public async Task DeletePerson(Guid id)
    {
        await unitOfWork.Persons.RemoveByIdAsync(id);
        await unitOfWork.SaveChangesAsync();
    }
    
    // Dodaje notatkę do osoby.
    public async Task<NoteDto> AddNoteToPerson(Guid personId, CreateNoteDto noteDto)
    {
        var person = await unitOfWork.Persons.FindByIdAsync(personId);
        if (person == null) 
            throw new ContactNotFoundException($"Person with id={personId} not found!");

        // Jeśli lista notatek jest nullem — inicjalizujemy.
        person.Notes ??= new List<Note>();

        // Tworzymy nową notatkę.
        var note = new Note 
        { 
            Content = noteDto.Content, 
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "System"
        };
    
        // Wymuszenie ID notatki.
        note.Id = Guid.NewGuid(); 

        person.Notes.Add(note);
    
        await unitOfWork.Persons.UpdateAsync(person);
        await unitOfWork.SaveChangesAsync();

        return mapper.Map<NoteDto>(note);
    }

    // Pobiera osobę po ID (alias GetById).
    public async Task<PersonDto> GetPerson(Guid personId)
    {
        var person = await unitOfWork.Persons.FindByIdAsync(personId);
        if (person == null) throw new ContactNotFoundException($"Person with id={personId} not found!");
        return mapper.Map<PersonDto>(person);
    }

    // Usuwa notatkę z osoby.
    public async Task DeleteNoteFromPerson(Guid personId, Guid noteId)
    {
        var person = await unitOfWork.Persons.FindByIdAsync(personId);
        if (person == null) 
            throw new ContactNotFoundException($"Person with id={personId} not found!");
    
        // Jeśli osoba nie ma notatek — nic nie robimy.
        if (person.Notes == null) return; 

        // Szukamy notatki.
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

using AppCore.DTOs;
using AppCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

// Piotr Bacior - WSEI Kraków

namespace WebApi.Controllers;

[ApiController]
[Route("api/contacts")]
public class ContactsController(IPersonService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllPersons(int page = 1, int size = 20)
    {
        return Ok(await service.FindAllPeoplePaged(page, size));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPerson(Guid id)
    {
        try
        {
            var dto = await service.GetById(id);
            return Ok(dto);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePersonDto dto)
    {
        var result = await service.CreatePerson(dto);
        return CreatedAtAction(nameof(GetPerson), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdatePersonDto dto)
    {
        try
        {
            var result = await service.UpdatePerson(id, dto);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await service.DeletePerson(id);
        return NoContent();     // Status 204: operacja wykonana, nie ma co zwracać w ciele
    }
    
    [HttpPost("{contactId:guid}/notes")]
    public async Task<IActionResult> AddNote([FromRoute] Guid contactId, [FromBody] CreateNoteDto dto)
    {
        // Wywołanie metody, którą zaimplementowaliśmy w MemoryPersonService
        var note = await service.AddNoteToPerson(contactId, dto);
    
        // Zwracamy status 201 (Created)
        return CreatedAtAction(nameof(GetNotes), new { contactId }, note);
    }

    [HttpGet("{contactId:guid}/notes")]
    [ProducesResponseType(typeof(IEnumerable<NoteDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNotes([FromRoute] Guid contactId)
    {
        // service.GetPerson zwraca PersonDto, które ma już właściwość Notes
        var person = await service.GetPerson(contactId);
        return Ok(person.Notes); 
    }
    
    [HttpDelete("{contactId:guid}/notes/{noteId:guid}")]
    public async Task<IActionResult> DeleteNote(Guid contactId, Guid noteId)
    {
        await service.DeleteNoteFromPerson(contactId, noteId);
        return NoContent();         // zwracamy 204 No Content - w przypadku braku usuwania notatki
    }

}
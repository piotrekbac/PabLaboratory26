using AppCore.DTOs;
using AppCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

// Piotr Bacior - WSEI Kraków

namespace WebApi.Controllers;

[ApiController]
[Route("api/contacts")]
public class ContactsController(IPersonService service) : ControllerBase
{
    // GET: api/contacts?page=1&size=20
    [HttpGet]
    public async Task<IActionResult> GetAllPersons(int page = 1, int size = 20)
    {
        return Ok(await service.FindAllPeoplePaged(page, size));
    }

    // GET: api/contacts/{id}
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

    // POST: api/contacts
    [HttpPost]
    public async Task<IActionResult> Create(CreatePersonDto dto)
    {
        var result = await service.CreatePerson(dto);
        return CreatedAtAction(nameof(GetPerson), new { id = result.Id }, result);
    }

    // PUT: api/contacts/{id}
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

    // DELETE: api/contacts/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await service.DeletePerson(id);
        return NoContent();
    }
    
    // POST: api/contacts/{contactId}/notes
    [HttpPost("{contactId:guid}/notes")]
    public async Task<IActionResult> AddNote(Guid contactId, CreateNoteDto dto)
    {
        var note = await service.AddNoteToPerson(contactId, dto);
        return CreatedAtAction(nameof(GetNotes), new { contactId }, note);
    }

    // GET: api/contacts/{contactId}/notes
    [HttpGet("{contactId:guid}/notes")]
    [ProducesResponseType(typeof(IEnumerable<NoteDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNotes(Guid contactId)
    {
        var person = await service.GetPerson(contactId);
        return Ok(person.Notes);
    }
    
    // DELETE: api/contacts/{contactId}/notes/{noteId}
    [HttpDelete("{contactId:guid}/notes/{noteId:guid}")]
    public async Task<IActionResult> DeleteNote(Guid contactId, Guid noteId)
    {
        await service.DeleteNoteFromPerson(contactId, noteId);
        return NoContent();
    }
}

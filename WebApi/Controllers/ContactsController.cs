using AppCore.DTOs;
using AppCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        return NoContent(); // Status 204: operacja wykonana, nie ma co zwracać w ciele
    }
}
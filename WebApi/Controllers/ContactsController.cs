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
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPerson(Guid id)
    {
        var person = await service.GetById(id);
        return person == null ? NotFound() : Ok(person);
    }
}
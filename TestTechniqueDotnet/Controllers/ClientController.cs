using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTechniqueDotnet.Context;
using TestTechniqueDotnet.Models;
using TestTechniqueDotnet.Models.Dto;

namespace TestTechniqueDotnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : Controller
{
    private readonly PetStoreContext _petStoreContext;
    private readonly ILogger<ClientController> _logger;

    public ClientController(PetStoreContext petStoreContext, ILogger<ClientController> logger)
    {
        _petStoreContext = petStoreContext;
        _logger = logger;
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Client> Get(Guid id)
    {
        var client = _petStoreContext.Clients.FirstOrDefault(x => x.Id == id);

        if (client is null)
        {
            _logger.LogInformation("No client for id : {id}", id);
            return NotFound();
        }
        
        return Ok(client);
    }
    
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Client>> Get()
    {
        var res = _petStoreContext.Clients
            .Include(x => x.Transactions)
            .Include(x => x.Animals).ToList();
        return Ok(res);
    }
    
    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Post([FromBody] ClientDTO clientDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        var client = clientDto.ToClient();
        _petStoreContext.Clients.Add(client);
        _petStoreContext.SaveChanges();
        return CreatedAtAction(nameof(Post), new { id = client.Id }, client);
    }
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Put(Guid id, [FromBody] ClientDTO clientDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var existingClient = _petStoreContext.Clients.FirstOrDefault(x => x.Id == id);
        if (existingClient is null)
        {
            _logger.LogInformation("No client for id : {id}", id);
            return NotFound();
        }

        existingClient.FirstName = clientDto.FirstName;
        existingClient.LastName = clientDto.LastName;
        _petStoreContext.Clients.Update(existingClient);
        _petStoreContext.SaveChanges();
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var existingClient = _petStoreContext.Clients.FirstOrDefault(x => x.Id == id);
        if (existingClient is null)
        {
            _logger.LogInformation("No client for id : {id}", id);
            return NotFound();
        }
        
        _petStoreContext.Clients.Remove(existingClient);
        _petStoreContext.SaveChanges();
        return NoContent();
    }
}
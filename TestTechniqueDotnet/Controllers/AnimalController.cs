using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTechniqueDotnet.Context;
using TestTechniqueDotnet.Models;
using TestTechniqueDotnet.Models.Dto;

namespace TestTechniqueDotnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalController : Controller
{
    private readonly PetStoreContext _petStoreContext;
    private readonly ILogger<AnimalController> _logger;

    public AnimalController(PetStoreContext petStoreContext, ILogger<AnimalController> logger)
    {
        _petStoreContext = petStoreContext;
        _logger = logger;
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Client> Get(Guid id)
    {
        var animal = _petStoreContext.Animals
            .Include(x => x.Transaction)
            // .Include(x => x.Master)
            .FirstOrDefault(x => x.Id == id);

        if (animal is null)
        {
            _logger.LogInformation("No animal for id : {id}", id);
            return NotFound();
        }
        
        return Ok(animal);
    }
    
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Animal>> Get()
    {
        var res = _petStoreContext.Animals
            .Include(x => x.Transaction)
            // .Include(x => x.Master)
            .ToList();
        return Ok(res);
    }
    
    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Post([FromBody] AnimalDTO animalDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        var animal = animalDto.ToAnimal();
        _petStoreContext.Animals.Add(animal);
        _petStoreContext.SaveChanges();
        return CreatedAtAction(nameof(Post), new { id = animal.Id }, animal);
    }
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Put(Guid id, [FromBody] AnimalDTO animalDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var existingAnimal = _petStoreContext.Animals.FirstOrDefault(x => x.Id == id);
        if (existingAnimal is null)
        {
            _logger.LogInformation("No animal for id : {id}", id);
            return NotFound();
        }

        existingAnimal.Name = animalDto.Name;
        existingAnimal.BirthDate = animalDto.BirthDate;
        _petStoreContext.Animals.Update(existingAnimal);
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

        var existingAnimal = _petStoreContext.Animals.FirstOrDefault(x => x.Id == id);
        if (existingAnimal is null)
        {
            _logger.LogInformation("No animal for id : {id}", id);
            return NotFound();
        }
        
        _petStoreContext.Animals.Remove(existingAnimal);
        _petStoreContext.SaveChanges();
        return NoContent();
    }
}
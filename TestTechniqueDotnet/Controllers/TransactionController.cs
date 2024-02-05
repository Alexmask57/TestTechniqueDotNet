using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTechniqueDotnet.Context;
using TestTechniqueDotnet.Models;
using TestTechniqueDotnet.Models.Dto;

namespace TestTechniqueDotnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : Controller
{
    private readonly PetStoreContext _petStoreContext;
    private readonly ILogger<TransactionController> _logger;
    
    public TransactionController(PetStoreContext petStoreContext, ILogger<TransactionController> logger)
    {
        _petStoreContext = petStoreContext;
        _logger = logger;
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Transaction> Get(Guid id)
    {
        var transaction = _petStoreContext.Transactions.FirstOrDefault(x => x.Id == id);
    
        if (transaction is null)
        {
            _logger.LogInformation("No transaction for id : {id}", id);
            return NotFound();
        }
        
        return Ok(transaction);
    }
    
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Transaction>> Get()
    {
        var res = _petStoreContext.Transactions;
        return Ok(res);
    }
    
    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Post([FromBody] TransactionDTO transactionDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        var existingClient = _petStoreContext.Clients.FirstOrDefault(x => x.Id == transactionDto.ClientId);
        if (existingClient is null)
        {
            _logger.LogInformation("No client for id : {id}", transactionDto.ClientId);
            return NotFound($"No client for id : {transactionDto.ClientId}");
        }
        
        var existingAnimal = _petStoreContext.Animals.FirstOrDefault(x => x.Id == transactionDto.AnimalId);
        if (existingAnimal is null)
        {
            _logger.LogInformation("No animal for id : {id}", transactionDto.AnimalId);
            return NotFound($"No animal for id : {transactionDto.AnimalId}");
        }
    
        if (existingAnimal.MasterId is not null)
        {
            _logger.LogInformation("Animal {id} has already a master", transactionDto.AnimalId);
            return BadRequest($"Animal {transactionDto.AnimalId} has already a master");
        }
        var transaction = transactionDto.ToTransaction();
        _petStoreContext.Attach(existingClient);
        _petStoreContext.Attach(existingAnimal);
        existingAnimal.Master = existingClient;
        
        _petStoreContext.Update(existingAnimal);
        _petStoreContext.Update(transaction);
        
        _petStoreContext.SaveChanges();
        return CreatedAtAction(nameof(Post), new { id = transaction.Id }, transaction);
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
    
        var existingTransaction = _petStoreContext.Transactions.FirstOrDefault(x => x.Id == id);
        if (existingTransaction is null)
        {
            _logger.LogInformation("No transaction for id : {id}", id);
            return NotFound();
        }
        
        _petStoreContext.Transactions.Remove(existingTransaction);
        _petStoreContext.SaveChanges();
        return NoContent();
    }
}
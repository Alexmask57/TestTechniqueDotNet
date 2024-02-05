using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using TestTechniqueDotnet.Context;

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
    
    // [HttpGet("{id:guid}")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public ActionResult<Transaction> Get(Guid id)
    // {
    //     var transaction = _petStoreContext.Transactions.FirstOrDefault(x => x.Id == id);
    //
    //     if (transaction is null)
    //     {
    //         _logger.LogInformation("No transaction for id : {id}", id);
    //         return NotFound();
    //     }
    //     
    //     return Ok(transaction);
    // }
}
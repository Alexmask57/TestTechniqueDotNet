using System.ComponentModel.DataAnnotations;

namespace TestTechniqueDotnet.Models.Dto;

public class TransactionDTO
{
    [Required]
    public decimal Price { get; set; }
    [Required]
    public Guid ClientId { get; set; }
    [Required]
    public Guid AnimalId { get; set; }
    
    public Transaction ToTransaction()
    {
        return new Transaction()
        {
            Price = Price,
            ClientId = ClientId,
            AnimalId = AnimalId
        };
    }
}
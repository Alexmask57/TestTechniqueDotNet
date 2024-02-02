namespace TestTechniqueDotnet.Models;

public class Transaction
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Price { get; set; }
    public Guid ClientId { get; set; }
    public Client Client { get; set; } 
    
    public Guid AnimalId { get; set; }
    public Animal Animal { get; set; }
}
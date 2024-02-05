namespace TestTechniqueDotnet.Models;

public class Transaction
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public decimal Price { get; set; }
    public Guid ClientId { get; set; }
    public Guid AnimalId { get; set; }
}
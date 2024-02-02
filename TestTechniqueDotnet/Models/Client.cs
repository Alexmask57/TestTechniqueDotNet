namespace TestTechniqueDotnet.Models;

public class Client
{
    public Guid Id { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public ICollection<Animal> Animals { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
}
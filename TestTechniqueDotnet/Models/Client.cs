using System.Text.Json.Serialization;

namespace TestTechniqueDotnet.Models;

public class Client
{
    public Guid Id { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public virtual ICollection<Animal> Animals { get; set; }
    public virtual ICollection<Transaction> Transactions { get; set; }
}
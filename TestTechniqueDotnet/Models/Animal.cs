namespace TestTechniqueDotnet.Models;

public class Animal
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public Client? Master { get; set; }
    public Transaction? Transaction { get; set; }
}
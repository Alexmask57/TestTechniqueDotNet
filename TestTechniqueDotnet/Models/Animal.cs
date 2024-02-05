namespace TestTechniqueDotnet.Models;

public class Animal
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public Guid? MasterId { get; set; }
    public virtual Client? Master { get; set; }
    public Transaction? Transaction { get; set; }
}
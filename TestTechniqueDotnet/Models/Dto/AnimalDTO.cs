using System.ComponentModel.DataAnnotations;

namespace TestTechniqueDotnet.Models.Dto;

public class AnimalDTO
{
    [Required]
    [MinLength(3)]
    public string Name { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
    
    public Animal ToAnimal()
    {
        return new Animal()
        {
            Name = Name,
            BirthDate = BirthDate
        };
    }
}
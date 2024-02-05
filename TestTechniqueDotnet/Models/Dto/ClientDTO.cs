using System.ComponentModel.DataAnnotations;

namespace TestTechniqueDotnet.Models.Dto;

public class ClientDTO
{
    [Required]
    [MinLength(3)]
    public string LastName { get; set; }
    
    [Required]
    [MinLength(3)]
    public string FirstName { get; set; }

    public Client ToClient()
    {
        return new Client()
        {
            LastName = LastName,
            FirstName = FirstName
        };
    }
}
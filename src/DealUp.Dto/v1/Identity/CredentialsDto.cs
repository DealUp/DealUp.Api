using System.ComponentModel.DataAnnotations;

namespace DealUp.Dto.v1.Identity;

public class CredentialsDto
{
    [Required, EmailAddress]
    public required string Username { get; set; }

    [Required, MinLength(8)]
    public required string Password { get; set; }
}
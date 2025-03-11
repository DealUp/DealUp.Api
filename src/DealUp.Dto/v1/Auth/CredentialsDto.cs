using System.ComponentModel.DataAnnotations;

namespace DealUp.Dto.v1.Auth;

public class CredentialsDto
{
    [Required, EmailAddress]
    public required string Username { get; set; }

    [Required, MinLength(8)]
    public required string Password { get; set; }

    [Required, Compare(nameof(Password))]
    public required string PasswordConfirmation { get; set; }
}
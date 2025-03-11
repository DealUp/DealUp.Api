using System.ComponentModel.DataAnnotations;
using DealUp.Dal.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DealUp.Dal;

[Index(nameof(Username), IsUnique = true)]
public class User : EntityBase
{
    [MaxLength(256)]
    public required string Username { get; set; }

    [MaxLength(64)]
    public string? Password { get; set; }

    [MaxLength(64)]
    public required string Status { get; set; }
}
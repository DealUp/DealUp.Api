using System.ComponentModel.DataAnnotations;

namespace DealUp.Dto.v1.Identity;

public class SsoProviderDto
{
    [Required(AllowEmptyStrings = false)]
    public required string Name { get; set; }
}
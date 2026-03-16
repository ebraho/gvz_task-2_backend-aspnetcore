using System.ComponentModel.DataAnnotations;

namespace GVZ.Task2BackendASPNETCore;

public class AdministrativeChangeMessageCreateDto
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public required string FirstName { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 1)]
    public required string LastName { get; set; }

    [Required]
    public required PostalAddress PostalAddress { get; set; }

    [Required]
    [StringLength(1024, MinimumLength = 1)]
    public required string FreeText { get; set; }
}

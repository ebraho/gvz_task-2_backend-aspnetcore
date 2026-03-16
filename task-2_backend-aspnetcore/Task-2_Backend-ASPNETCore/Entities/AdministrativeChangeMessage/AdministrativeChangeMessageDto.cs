using System.ComponentModel.DataAnnotations;

namespace GVZ.Task2BackendASPNETCore;

public class AdministrativeChangeMessageDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }

    [Required]
    public required PostalAddress PostalAddress { get; set; }

    [Required]
    public required string FreeText { get; set; }
}

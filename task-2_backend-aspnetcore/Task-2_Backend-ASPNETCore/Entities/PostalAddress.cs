using System.ComponentModel.DataAnnotations;

namespace GVZ.Task2BackendASPNETCore;

public class PostalAddress
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public required string Street { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "HouseNumber must be a positive number.")]
    public required int HouseNumber { get; set; }

    [Required]
    [RegularExpression(@"^\d{4}$", ErrorMessage = "ZipCode must be a 4-digit number.")]
    public required string ZipCode { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 1)]
    public required string City { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 1)]
    public required string Canton { get; set; }
}

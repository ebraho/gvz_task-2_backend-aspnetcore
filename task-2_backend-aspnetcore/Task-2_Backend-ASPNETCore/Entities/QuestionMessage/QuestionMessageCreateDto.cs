using System.ComponentModel.DataAnnotations;

namespace GVZ.Task2BackendASPNETCore;

public class QuestionMessageCreateDto
{
    [Required]
    [StringLength(1024, MinimumLength = 1)]
    public required string Question { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace GVZ.Task2BackendASPNETCore;

public class QuestionMessageDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public required string Question { get; set; }
}

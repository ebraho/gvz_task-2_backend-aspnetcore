using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GVZ.Task2BackendASPNETCore;

public class QuestionMessage
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(1024, MinimumLength = 1)]
    public required string Question { get; set; }

    [Required]
    public required DateTime SubmittedAt { get; set; }
}

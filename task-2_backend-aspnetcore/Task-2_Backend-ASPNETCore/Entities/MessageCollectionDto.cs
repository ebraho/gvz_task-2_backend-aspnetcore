using System.ComponentModel.DataAnnotations;

namespace GVZ.Task2BackendASPNETCore;

public class MessageCollectionDto
{
    [Required]
    public List<QuestionMessageDto> QuestionMessages { get; set; } = [];

    [Required]
    public List<AdministrativeChangeMessageDto> AdministrativeChangeMessages { get; set; } = [];
}

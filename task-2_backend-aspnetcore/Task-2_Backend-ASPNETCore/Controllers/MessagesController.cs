using System.Net.Mime;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GVZ.Task2BackendASPNETCore;

[ApiController]
[Route("[controller]")]
public class MessagesController(IMapper mapper, MessagesContext messagesContext) : ControllerBase
{
    [HttpGet()]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<MessageCollectionDto>> GetMessages()
    {
        var messageCollectionDto = new MessageCollectionDto
        {
            QuestionMessages = mapper.Map<List<QuestionMessageDto>>(
                await messagesContext.QuestionMessages.ToListAsync()
            )
        };

        return Ok(messageCollectionDto);
    }

    [HttpPost("question")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<QuestionMessageDto>> PostQuestionMessage(
        [FromBody] QuestionMessageCreateDto questionMessageCreateDto
    )
    {
        QuestionMessage questionMessage = mapper.Map<QuestionMessage>(questionMessageCreateDto);

        await messagesContext.QuestionMessages.AddAsync(questionMessage);
        await messagesContext.SaveChangesAsync();

        var questionMessageDto = mapper.Map<QuestionMessageDto>(questionMessage);

        return Ok(questionMessageDto);
    }
}
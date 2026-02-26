using Business;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/messages")]
    public class MessageController : Controller
    {
        readonly IMessageService _service;
        public MessageController(IMessageService service)
        { 
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Send(int senderId,SendMessageDto dto)
        {
            await _service.SendAsync(senderId, dto);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> Conversation(int userId, int otherUserId)
        {
            return Ok(await _service.GetConversationAsync(userId,otherUserId));
        }
    }
}

using Business;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]  
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _service;

        public MessagesController(IMessageService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Send(SendMessageDto dto)
        {
            var senderId = GetCurrentUserId();
            await _service.SendMessageAsync(senderId, dto);
            return Ok();
        }

        [HttpGet("conversation")]
        public async Task<IActionResult> GetConversation([FromQuery] int otherUserId)
        {
            var userId = GetCurrentUserId();
            var messages = await _service.GetConversationAsync(userId, otherUserId);
            return Ok(messages);
        }

        int GetCurrentUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null || !int.TryParse(claim.Value, out var userId))
                throw new UnauthorizedAccessException("User ID not found in token.");
            return userId;
        }
    }
}
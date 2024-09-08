using System.Net.WebSockets;
using InstagramCloneAPI.Dtos;
using InstagramCloneAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InstagramCloneAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagingController(ApplicationDbContext context) : ControllerBase
    {
        // GET: api/Messaging/chats
        [HttpGet("chats")]
        public async Task<ActionResult<IEnumerable<ChatDto>>> GetChats()
        {
            // Get the current user's ID from the authenticated token
            // var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // if (string.IsNullOrEmpty(userId))
            // {
            //     return Unauthorized();
            // }

            var userId = "abc";

            var chats = await context.Chats
                .Where(c => c.UserIds.Any(id => id == userId))
                .Select(c => new ChatDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    // Add other relevant properties
                })
                .ToListAsync();

            return Ok(chats);
        }

        // POST: api/Messaging/chats
        [HttpPost("chats")]
        public async Task<ActionResult<ChatDto>> CreateChat(ChatDto requestChatDto)
        {
            var chat = new Chat
            {
                Name = requestChatDto.Name,
                UserIds = requestChatDto.UserIds
            };

            context.Chats.Add(chat);
            await context.SaveChangesAsync();

            var responseChatDto = new ChatDto
            {
                Id = chat.Id,
                Name = chat.Name,
                UserIds = chat.UserIds
            };

            return CreatedAtAction(nameof(GetChat), new { id = chat.Id }, responseChatDto);
        }

        // TODO: Should take in a paging parameters
        // GET: api/Messaging/chats/{id}/messages
        [HttpGet("chats/{id}/messages")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetChatMessages(string id)
        {
            var messages = await context.Messages
                .Where(m => m.ChatId == id)
                .OrderBy(m => m.Timestamp)
                .Select(m => new MessageDto
                {
                    Id = m.Id,
                    Content = m.Content,
                    SenderId = m.SenderId,
                    Timestamp = m.Timestamp
                })
                .ToListAsync();

            return Ok(messages);
        }

        // WebSocket: /ws
        [Route("/ws")]
        public async Task HandleWebSocket()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await Echo(webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

        private async Task Echo(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            var receiveResult = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!receiveResult.CloseStatus.HasValue)
            {
                await webSocket.SendAsync(
                    new ArraySegment<byte>(buffer, 0, receiveResult.Count),
                    receiveResult.MessageType,
                    receiveResult.EndOfMessage,
                    CancellationToken.None);

                receiveResult = await webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await webSocket.CloseAsync(
                receiveResult.CloseStatus.Value,
                receiveResult.CloseStatusDescription,
                CancellationToken.None);
        }

        // Helper method to get a single chat (used in CreateChat)
        [HttpGet("chats/{id}")]
        public async Task<ActionResult<ChatDto>> GetChat(string id)
        {
            var chat = await context.Chats
                .Where(c => c.Id == id)
                .Select(c => new
                    ChatDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        // Add other relevant properties
                    })
                .FirstOrDefaultAsync();

            if (chat == null)
            {
                return NotFound();
            }

            return Ok(chat);
        }
    }
}
using Business.Interfaces;
using Data;
using DTOs;
using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;


namespace Business
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _repo;
        private readonly IMessageNotifier _notifier;
        private readonly ILogger<MessageService> _logger;
        private readonly IConfiguration _configuration;

        public MessageService(IMessageRepository repo, IMessageNotifier notifier, ILogger<MessageService> logger, IConfiguration configuration)
        {
            _repo = repo;
            _notifier = notifier;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task SendMessageAsync(int senderId, SendMessageDto dto)
        {
            try
            {
                var entity = new MessageEntity
                {
                    SenderId = senderId,
                    ReceiverId = dto.ReceiverId,
                    Message = dto.Message
                };

                await _repo.CreateMessageAsync(entity);

                var messageDto = new MessageDto
                {
                    Id = entity.Id,
                    SenderId = senderId,
                    ReceiverId = dto.ReceiverId,
                    Message = dto.Message,
                    SentAt = entity.SentAt
                };

                await _notifier.NotifyAsync(dto.ReceiverId, messageDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send message from {senderId} to {dto.ReceiverId}");
                throw;
            }
        }

        public async Task<List<MessageDto>> GetConversationAsync(int userId, int otherUserId)
        {
            try
            {
                var data = await _repo.GetChatAsync(userId, otherUserId);
                return data.Select(m => new MessageDto
                {
                    Id = m.Id,
                    SenderId = m.SenderId,
                    ReceiverId = m.ReceiverId,
                    SenderUsername = m.Sender.Username,
                    ReceiverUsername = m.Receiver.Username,
                    Message = m.Message,
                    SentAt = m.SentAt
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve conversation between {UserId} and {OtherUserId}", userId, otherUserId);
                throw;
            }
        }
        public async Task<object?> SendAIMessageAsync(int ReceiverId, SendAIMessageDto dto)
        {
            string apiKey = Environment.GetEnvironmentVariable("AIChatKey") ?? _configuration["API:AIChatKey"] ?? "";
            string apiUrl = "https://api.groq.com/openai/v1/chat/completions";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var requestBody = new
            {
                model = "llama-3.1-8b-instant",
                messages = new[] { new { role = "user", content = dto.Message } },
                max_tokens = 1024,
                temperature = 0.7
            };

            try
            {
                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(apiUrl, content);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(responseBody);
                var reply = doc.RootElement
                               .GetProperty("choices")[0]
                               .GetProperty("message")
                               .GetProperty("content")
                               .GetString();

                var entity = new AIMessageEntity
                {
                    Input = dto.Message,
                    Output = reply,
                    ReceiverId = ReceiverId,
                    SentAt = DateTime.UtcNow
                };
                await _repo.CreateAIChatAsync(entity);

                if (reply != null)
                    return reply;
                return null;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send message to AI from {ReceiverId}");
                throw;
            }

        }
    }
}
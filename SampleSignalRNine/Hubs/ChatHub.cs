using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SampleSignalRNine.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SampleSignalRNine.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(ApplicationDbContext dbContext, ILogger<ChatHub> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task SendMessageToAll(string user, string message)
        {
            try
            { 
                await Clients.All.SendAsync("MessageReceived", user, message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while sending message to all users.");
            }
        }

        public async Task SendMessageToReceiver(string user, string receiver, string message)
        {
            try
            {
                 
                var receiverId = _dbContext.Users.FirstOrDefault(u => u.Email.ToLower() == receiver.ToLower());

                if (receiverId != null)
                { 
                    await Clients.User(receiverId.Id).SendAsync("MessageReceived", user, message);
                }
                else
                {
                    _logger.LogWarning("Receiver not found in the database.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while sending message to receiver.");
            }
        }
    }
}

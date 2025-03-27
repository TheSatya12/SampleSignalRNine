using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleSignalRNine.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly ILogger<NotificationHub> _logger;
        public static int notificationCounter = 0;
        public static List<string> messages = new();

        public NotificationHub(ILogger<NotificationHub> logger)
        {
            _logger = logger;
        }

        public async Task SendMessage(string message)
        {
            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    notificationCounter++;
                    messages.Add(message);
                    await LoadMessages();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SendMessage method");
            }
        }

        public async Task LoadMessages()
        {
            try
            {
                await Clients.All.SendAsync("LoadNotification", messages, notificationCounter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LoadMessages method");
            }
        }
    }
}

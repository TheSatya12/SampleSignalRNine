using Microsoft.AspNetCore.SignalR;

namespace SampleSignalRNine.Hubs
{
    public class UserHub : Hub
    {
        private readonly ILogger<UserHub> _logger;

        public static int TotalViews { get; set; } = 0;
        public static int TotalUsers { get; set; } = 0;

        public UserHub(ILogger<UserHub> logger)
        {
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                TotalUsers++; 
                await Clients.All.SendAsync("updateTotalUsers", TotalUsers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OnConnectedAsync.");
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            try
            {
                TotalUsers--; 
                await Clients.All.SendAsync("updateTotalUsers", TotalUsers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OnDisconnectedAsync.");
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task<string> NewWindowLoaded(string name)
        {
            try
            {
                TotalViews++;
                await Clients.All.SendAsync("updateTotalViews", TotalViews);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in NewWindowLoaded.");
            }
            return $"total Views {name} - {TotalViews}";
        }
    }
}

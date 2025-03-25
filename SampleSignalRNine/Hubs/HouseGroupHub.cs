using Microsoft.AspNetCore.SignalR;

namespace SampleSignalRNine.Hubs
{
    public class HouseGroupHub : Hub
    {
        private readonly ILogger<HouseGroupHub> _logger;
        public static List<string> GroupsJoined { get; set; } = new List<string>();

        public HouseGroupHub(ILogger<HouseGroupHub> logger)
        {
            _logger = logger;
        }

        public async Task JoinHouse(string houseName)
        {
            try
            {
                if (!GroupsJoined.Contains(Context.ConnectionId + ":" + houseName))
                {
                    GroupsJoined.Add(Context.ConnectionId + ":" + houseName);

                    string houseList = "";
                    foreach (var str in GroupsJoined)
                    {
                        if (str.Contains(Context.ConnectionId))
                        {
                            houseList += str.Split(':')[1] + " ";
                        }
                    }

                    await Clients.Caller.SendAsync("subscriptionStatus", houseList, houseName.ToLower(), true);
                    await Clients.Others.SendAsync("newMemberAddedToHouse", houseName);
                    await Groups.AddToGroupAsync(Context.ConnectionId, houseName);

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while joining house {HouseName}", houseName);
            }
        }

        public async Task LeaveHouse(string houseName)
        {
            try
            {
                if (GroupsJoined.Contains(Context.ConnectionId + ":" + houseName))
                {
                    GroupsJoined.Remove(Context.ConnectionId + ":" + houseName);

                    string houseList = "";
                    foreach (var str in GroupsJoined)
                    {
                        if (str.Contains(Context.ConnectionId))
                        {
                            houseList += str.Split(':')[1] + " ";
                        }
                    }

                    await Clients.Caller.SendAsync("subscriptionStatus", houseList, houseName.ToLower(), false);
                    await Clients.Others.SendAsync("newMemberRemovedFromHouse", houseName);
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, houseName);

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while leaving house {HouseName}", houseName);
            }
        }

        public async Task TriggerHouseNotify(string houseName)
        {
            await Clients.Group(houseName).SendAsync("triggerHouseNotification", houseName);
        }
    }
}

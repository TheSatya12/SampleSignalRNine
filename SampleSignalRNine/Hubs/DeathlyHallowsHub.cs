using Microsoft.AspNetCore.SignalR;

namespace SampleSignalRNine.Hubs
{
    public class DeathlyHallowsHub:Hub
    {
        public Dictionary<string, int> GetRaceStatus()
        {
            return SD.DeathlyHallowRace;
        }
    }
}

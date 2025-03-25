using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SampleSignalRNine.Hubs;
using SampleSignalRNine.Models;

namespace SampleSignalRNine.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<DeathlyHallowsHub> _deathlyHub;

        public HomeController(ILogger<HomeController> logger,IHubContext<DeathlyHallowsHub> deathlyHub)
        {
            _logger = logger;
            _deathlyHub = deathlyHub;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DeathlyHallows(string type)
        {
            try
            {
                if (SD.DeathlyHallowRace.ContainsKey(type))
                {
                    SD.DeathlyHallowRace[type]++;
                }

                await _deathlyHub.Clients.All.SendAsync("updateDeathlyHallowCount",
                    SD.DeathlyHallowRace[SD.Cloak],
                    SD.DeathlyHallowRace[SD.Stone],
                    SD.DeathlyHallowRace[SD.Wand]);
                 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating Deathly Hallow count for {Type}", type);
            }
            return Accepted();
        }

        public async Task<IActionResult> UserCount()
        {
            return View();
        }
        public async Task<IActionResult> HouseGroup()
        {
            return View();
        }
        [HttpGet]
        public IActionResult DeathlyHallows()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Notification()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

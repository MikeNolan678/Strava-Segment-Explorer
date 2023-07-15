using Microsoft.AspNetCore.Mvc;
using StravaSegmentExplorerUI.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace StravaSegmentExplorerUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public const string UserIdKey = "UserIdKey";


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [RequireHttps]
        [HttpGet]
        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (userId != null)
            {
                HttpContext.Session.SetString(UserIdKey, userId);
            }

            return View();
        }

        public IActionResult Privacy()
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
using Microsoft.AspNetCore.Mvc;

namespace ExpansesApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // Jeœli nie potrzebujesz ErrorViewModel, usuñ ten fragment
        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    }
}

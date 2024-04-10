using GeneratorAPI.Models;
using GeneratorASP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GeneratorASP.Controllers
{
    public class HomeController : Controller
        
    {
        private readonly AppDbContext db = new AppDbContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var c = db.Questions.ToArray();
            return View(c);
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

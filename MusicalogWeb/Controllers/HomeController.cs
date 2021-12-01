using Microsoft.AspNetCore.Mvc;
using MusicalogWeb.Interfaces.Services;
using MusicalogWeb.ViewModels;
using System.Diagnostics;

namespace MusicalogWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMusicalogAPIService _service;

        public HomeController(IMusicalogAPIService service)
        {
            _service = service;
        }

        public IActionResult Index()
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
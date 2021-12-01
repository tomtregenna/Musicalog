using Microsoft.AspNetCore.Mvc;
using MusicalogWeb.Interfaces.Services;
using MusicalogWeb.Models.MusicalogAPI;
using MusicalogWeb.ViewModels;
using System.Diagnostics;

namespace MusicalogWeb.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly IMusicalogAPIService _service;

        public ArtistsController(IMusicalogAPIService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var formats = await _service.GetFormatsAsync();
            var artists = await _service.GetArtistsAsync();

            var model = new ArtistsViewModel()
            {
                Formats = formats,
                Artists = artists
            };

            return View(model);
        }

        [HttpGet]
        [ActionName("Add")]
        public async Task<IActionResult> AddAsync()
        {
            return View(new ArtistsViewModel());
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> AddAsync(string Name)
        {
            var artist = new Artist()
            {
                Name = Name
            };

            var model = new ArtistsViewModel()
            {
                NewArtist = await _service.CreateArtistAsync(artist)
            };

            return View(model);
        }

        public async Task<IActionResult> Artist(Guid Id)
        {
            ViewData["artist"] = await _service.GetArtistAsync(Id);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
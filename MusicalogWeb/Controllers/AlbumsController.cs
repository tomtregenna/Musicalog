using Microsoft.AspNetCore.Mvc;
using MusicalogWeb.Interfaces.Services;
using MusicalogWeb.Models.MusicalogAPI;
using MusicalogWeb.ViewModels;
using System.Diagnostics;

namespace MusicalogWeb.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IMusicalogAPIService _service;

        public AlbumsController(IMusicalogAPIService service)
        {
            _service = service;
        }

        [HttpGet]
        [ActionName("Index")]
        public async Task<IActionResult> IndexAsync()
        {
            var formats = await _service.GetFormatsAsync();
            var albums = await _service.GetAlbumsAsync();

            var model = new AlbumsViewModel()
            {
                Formats = formats,
                Albums = albums
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Index")]
        public async Task<IActionResult> FilterAsync(string Title, string Artist)
        {
            var filters = new Dictionary<string, string>();

            filters.Add("Title", Title);
            filters.Add("Artist", Artist);

            var formats = await _service.GetFormatsAsync();
            var albums = await _service.GetAlbumsAsync(filters);

            var model = new AlbumsViewModel()
            {
                Formats = formats,
                Albums = albums
            };

            return View(model);
        }

        [HttpGet("{Id}")]
        [ActionName("Album")]
        public async Task<IActionResult> AlbumAsync(Guid Id)
        {
            ViewData["album"] = await _service.GetAlbumAsync(Id);

            return View();
        }

        [HttpGet]
        [ActionName("Add")]
        public async Task<IActionResult> AddAsync()
        {
            var formats = await _service.GetFormatsAsync();
            var artists = await _service.GetArtistsAsync();

            var model = new AlbumsViewModel()
            {
                Formats = formats,
                Artists = artists
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> AddAsync(string Title, Guid ArtistDD, string Artist, int Format, string Stock)
        {
            if (!Int32.TryParse(Stock, out int stock))
                return BadRequest("Stock must be a number");

            if (stock < 0)
                return BadRequest("Stock cannot be negative");

            var artist = (String.IsNullOrEmpty(Artist))
                ? await _service.GetArtistAsync(ArtistDD)
                : await _service.CreateArtistAsync(new Artist() { Name = Artist });

            var album = new Album()
            {
                Title = Title,
                ArtistId = artist.Id,
                FormatId = Format,
                Stock = stock
            };

            var formats = await _service.GetFormatsAsync();
            var artists = await _service.GetArtistsAsync();
            var newAlbum = await _service.CreateAlbumAsync(album);

            var model = new AlbumsViewModel()
            {
                Formats = formats,
                Artists = artists,
                NewAlbum = newAlbum,
                RedirectFromAction = true
            };

            return View(model);
        }

        [HttpPost("{Id}")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            var removedAlbum = await _service.DeleteAlbumAsync(Id);

            if (removedAlbum != null)
            {
                var model = new AlbumsViewModel()
                {
                    RemovedAlbum = removedAlbum
                };

                return View(model);
            }

            return View(new AlbumsViewModel());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
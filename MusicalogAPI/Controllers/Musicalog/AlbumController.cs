using Microsoft.AspNetCore.Mvc;
using MusicalogAPI.Interfaces.Musicalog.Repositories;
using MusicalogAPI.Models.Musicalog;
using System.Text;
using System.Text.Json;

namespace MusicalogAPI.Controllers.Musicalog
{
    [ApiController]
    [Route("api/albums/")]
    public class AlbumController : ControllerBase
    {
        private readonly IRepository<Album> _repository;

        public AlbumController(IRepository<Album> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Album>> GetAsync(bool includeGraph = true)
        {
            var filters = new Dictionary<string, string>();
            var bodyText = String.Empty;

            if (Request != null)
            {
                using (var reader = new StreamReader(Request.Body, Encoding.UTF8))

                    bodyText = await reader.ReadToEndAsync();

                // Filter names and values should be sent as in Json format as the request Body

                if (bodyText != String.Empty)
                    filters = JsonSerializer.Deserialize<Dictionary<string, string>>(bodyText);
            }

            return await _repository.GetAsync(includeGraph, filters);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Album?>> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> InsertAsync()
        {
            IActionResult result;

            var newAlbum = Request.ReadFromJsonAsync<Album>().Result;

            if (newAlbum == null)
                result = new BadRequestObjectResult("Album data could not be read from the request.");

            result = await _repository.InsertAsync(newAlbum);

            return result;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync()
        {
            IActionResult result;

            var newAlbum = Request.ReadFromJsonAsync<Album>().Result;

            if (newAlbum == null)
                result = new BadRequestObjectResult("Album data could not be read from the request.");

            result = await _repository.UpdateAsync(newAlbum);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
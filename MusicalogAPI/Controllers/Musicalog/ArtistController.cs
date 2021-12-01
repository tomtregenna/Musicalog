using Microsoft.AspNetCore.Mvc;
using MusicalogAPI.Interfaces.Musicalog.Repositories;
using MusicalogAPI.Models.Musicalog;
using System.Text;
using System.Text.Json;

namespace MusicalogAPI.Controllers.Musicalog
{
    [ApiController]
    [Route("api/artists/")]
    public class ArtistController : ControllerBase
    {
        private readonly IRepository<Artist> _repository;

        public ArtistController(IRepository<Artist> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Artist>> GetAsync(bool includeGraph = true)
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
        public async Task<ActionResult<Artist?>> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> InsertAsync()
        {
            IActionResult result;

            var newArtist = Request.ReadFromJsonAsync<Artist>().Result;

            if (newArtist == null)
                result = new BadRequestObjectResult("Artist data could not be read from the request.");

            result = await _repository.InsertAsync(newArtist);

            return result;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync()
        {
            IActionResult result;

            var newArtist = Request.ReadFromJsonAsync<Artist>().Result;

            if (newArtist == null)
                result = new BadRequestObjectResult("Artist data could not be read from the request.");

            result = await _repository.UpdateAsync(newArtist);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}

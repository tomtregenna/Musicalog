using Microsoft.AspNetCore.Mvc;
using MusicalogAPI.Interfaces.Musicalog.Repositories;
using MusicalogAPI.Models.Musicalog;

namespace MusicalogAPI.Controllers.Musicalog
{
    [ApiController]
    [Route("api/formats/")]
    public class FormatController : ControllerBase
    {
        private readonly IReadOnlyRepository<Format> _repository;

        public FormatController(IReadOnlyRepository<Format> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Format>> GetAsync()
        {
            return await _repository.GetAsync();
        }
    }
}
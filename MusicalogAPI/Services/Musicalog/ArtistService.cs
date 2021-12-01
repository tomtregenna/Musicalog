using Microsoft.AspNetCore.Mvc;
using MusicalogAPI.Interfaces.Musicalog.Repositories;
using MusicalogAPI.Interfaces.Musicalog.Services;
using MusicalogAPI.Models.Musicalog;

namespace MusicalogAPI.Services.Musicalog
{
    public class ArtistService : IArtistService
    {
        private readonly IRepository<Artist> _artistRepository;

        public ArtistService(IRepository<Artist> artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public Task<IActionResult> DeleteAsync(Guid id) => _artistRepository.DeleteAsync(id);

        public Task<IEnumerable<Artist>> GetAsync(bool includeGraph, Dictionary<string, string> filters) => _artistRepository.GetAsync(includeGraph, filters);

        public Task<ActionResult<Artist?>> GetByIdAsync(Guid id) => _artistRepository.GetByIdAsync(id);

        public Task<IActionResult> InsertAsync(Artist t) => _artistRepository.InsertAsync(t);

        public Task<IActionResult> UpdateAsync(Artist t) => _artistRepository.UpdateAsync(t);
    }
}

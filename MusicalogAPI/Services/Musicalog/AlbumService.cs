using Microsoft.AspNetCore.Mvc;
using MusicalogAPI.Interfaces.Musicalog.Repositories;
using MusicalogAPI.Interfaces.Musicalog.Services;
using MusicalogAPI.Models.Musicalog;

namespace MusicalogAPI.Services.Musicalog
{
    public class AlbumService : IAlbumService
    {
        private readonly IRepository<Album> _albumRepository;

        public AlbumService(IRepository<Album> albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public Task<IActionResult> DeleteAsync(Guid id) => _albumRepository.DeleteAsync(id);

        public Task<IEnumerable<Album>> GetAsync(bool includeGraph, Dictionary<string, string> filters) => _albumRepository.GetAsync(includeGraph, filters);

        public Task<ActionResult<Album?>> GetByIdAsync(Guid id) => _albumRepository.GetByIdAsync(id);

        public Task<IActionResult> InsertAsync(Album t) => _albumRepository.InsertAsync(t);

        public Task<IActionResult> UpdateAsync(Album t) => _albumRepository.UpdateAsync(t);
    }
}

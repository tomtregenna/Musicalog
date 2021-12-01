using Microsoft.AspNetCore.Mvc;
using MusicalogAPI.Models.Musicalog;

namespace MusicalogAPI.Interfaces.Musicalog.Services
{
    public interface IAlbumService
    {
        Task<IEnumerable<Album>> GetAsync(bool includeGraph, Dictionary<string, string> filters);
        Task<ActionResult<Album?>> GetByIdAsync(Guid id);

        Task<IActionResult> InsertAsync(Album t);

        Task<IActionResult> UpdateAsync(Album t);

        Task<IActionResult> DeleteAsync(Guid id);
    }
}

using Microsoft.AspNetCore.Mvc;
using MusicalogAPI.Models.Musicalog;

namespace MusicalogAPI.Interfaces.Musicalog.Services
{
    public interface IArtistService
    {
        Task<IEnumerable<Artist>> GetAsync(bool includeGraph, Dictionary<string, string> filters);
        Task<ActionResult<Artist?>> GetByIdAsync(Guid id);

        Task<IActionResult> InsertAsync(Artist t);

        Task<IActionResult> UpdateAsync(Artist t);

        Task<IActionResult> DeleteAsync(Guid id);
    }
}

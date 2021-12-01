using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicalogAPI.Interfaces.Musicalog.Repositories;
using MusicalogAPI.Models.Musicalog;

namespace MusicalogAPI.Repositories.Musicalog
{
    public class ArtistRepository : IRepository<Artist>
    {
        private readonly MusicalogContext _context;

        public ArtistRepository(MusicalogContext context)
        {
            _context = context;
        }

        #region Read Operations

        public async Task<IEnumerable<Artist>> GetAsync(bool includeGraph, Dictionary<string, string> filters)
        {
            var query = _context.Artists.AsQueryable();

            // Decide if related tables should be included (optional so only affects performance when specifically required)
            if (includeGraph)
                query = query.Include(a => a.Albums);

            return await Task.FromResult(query.ToList());
        }

        public async Task<ActionResult<Artist?>> GetByIdAsync(Guid id)
        {
            return await Task.FromResult(
                _context.Artists
                    .Include(a => a.Albums)
                    .ThenInclude(a => a.FormatIdNavigation)
                    .SingleOrDefault(a => a.Id == id));
        }

        #endregion

        #region Insert Operations

        public async Task<IActionResult> InsertAsync(Artist t)
        {
            try
            {
                _context.Artists.Add(t);

                var changes = await _context.SaveChangesAsync();

                if (changes == 0)
                    return new ObjectResult("No changes were written to the Database.");

                return new ObjectResult(t);
            }
            catch (DbUpdateException ex)
            {
                return new ObjectResult(ex.Message);
            }
        }

        #endregion

        #region Update Operations

        public async Task<IActionResult> UpdateAsync(Artist t)
        {
            var artist = await Task.FromResult(_context.Artists.SingleOrDefault(a => a.Id == t.Id));

            if (artist != null)
            {
                try
                {
                    _context.Artists.Update(t);

                    var changes = await _context.SaveChangesAsync();

                    if (changes == 0)
                        return new ObjectResult("No changes were written to the Database.");

                    return new ObjectResult(t);
                }
                catch (DbUpdateException ex)
                {
                    return new ObjectResult(ex.Message);
                }
            }

            return new NotFoundObjectResult(t.Id);
        }

        #endregion

        #region Delete Operations

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var artist = await Task.FromResult(_context.Artists.Include(a => a.Albums).SingleOrDefault(a => a.Id == id));

            if (artist != null)
            {
                try
                {
                    _context.Artists.Remove(artist);

                    var changes = await _context.SaveChangesAsync();

                    if (changes == 0)
                        return new ObjectResult("No changes were written to the Database.");

                    return new ObjectResult(artist);
                }
                catch (Exception ex)
                {
                    return new ObjectResult(ex.Message);
                }
            }

            return new NotFoundObjectResult(id);
        }

        #endregion
    }
}

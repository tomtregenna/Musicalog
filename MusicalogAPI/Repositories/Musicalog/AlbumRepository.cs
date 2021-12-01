using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicalogAPI.Interfaces.Musicalog.Repositories;
using MusicalogAPI.Models.Musicalog;

namespace MusicalogAPI.Repositories.Musicalog
{
    public class AlbumRepository : IRepository<Album>
    {
        private readonly MusicalogContext _context;

        public AlbumRepository(MusicalogContext context)
        {
            _context = context;
        }

        #region Read Operations

        public async Task<IEnumerable<Album>> GetAsync(bool includeGraph, Dictionary<string, string> filters)
        {
            var queries = new List<IQueryable<Album>>();

            if (!(String.IsNullOrWhiteSpace(filters.GetValueOrDefault("Title")))) {
                queries.Add(_context.Albums
                    .Where(a =>
                        a.Title
                            .ToLower()
                            .Contains(filters.GetValueOrDefault("Title").ToLower())
                    )
                );
            }

            if (!(String.IsNullOrWhiteSpace(filters.GetValueOrDefault("Artist")))) {
                queries.Add(_context.Albums
                    .Where(a =>
                        a.Artist.Name
                            .ToLower()
                            .Contains(filters.GetValueOrDefault("Artist").ToLower())
                    )
                );
            }

            // Build the final query based on available filters
            var query = queries.Count switch {
                2 => queries[0].Intersect(queries[1]),
                1 => queries[0],
                _ => _context.Albums,
            };

            // Decide if related tables should be included (optional so only affects performance when specifically required)
            if (includeGraph)
                query = query.Include(a => a.Artist);

            return await Task.FromResult(query.ToList());
        }

        public async Task<ActionResult<Album?>> GetByIdAsync(Guid id)
        {
            return await Task.FromResult(
                _context.Albums
                    .Include(a => a.Artist)
                    .SingleOrDefault(a => a.Id == id)
            );
        }

        #endregion

        #region Insert Operations

        public async Task<IActionResult> InsertAsync(Album t)
        {
            try
            {
                _context.Albums.Add(t);

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

        public async Task<IActionResult> UpdateAsync(Album t)
        {
            var album = await Task.FromResult(_context.Albums.SingleOrDefault(a => a.Id == t.Id));

            if (album != null)
            {
                try
                {
                    _context.Albums.Update(t);

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
            var album = await Task.FromResult(_context.Albums.SingleOrDefault(a => a.Id == id));

            if (album != null)
            {
                try
                {
                    _context.Albums.Remove(album);

                    var changes = await _context.SaveChangesAsync();

                    if (changes == 0)
                        return new ObjectResult("No changes were written to the Database.");

                    return new ObjectResult(album);
                }
                catch (DbUpdateException ex)
                {
                    return new ObjectResult(ex.Message);
                }
            }

            return new NotFoundObjectResult(id);
        }

        #endregion
    }
}

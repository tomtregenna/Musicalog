using MusicalogAPI.Interfaces.Musicalog.Repositories;
using MusicalogAPI.Models.Musicalog;

namespace MusicalogAPI.Repositories.Musicalog
{
    public class FormatRepository : IReadOnlyRepository<Format>
    {
        private readonly MusicalogContext _context;

        public FormatRepository(MusicalogContext context)
        {
            _context = context;
        }

        #region Read Operations

        // Get a list of available Formats, primarilly for building drop-down selections for forms
        public async Task<IEnumerable<Format>> GetAsync()
        {
            return await Task.FromResult(_context.Formats.ToList());
        }

        #endregion
    }
}
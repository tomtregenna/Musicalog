using MusicalogAPI.Interfaces.Musicalog.Repositories;
using MusicalogAPI.Interfaces.Musicalog.Services;
using MusicalogAPI.Models.Musicalog;

namespace MusicalogAPI.Services.Musicalog
{
    public class FormatService : IFormatService
    {
        private readonly IReadOnlyRepository<Format> _formatRepository;

        public FormatService(IReadOnlyRepository<Format> formatRepository)
        {
            _formatRepository = formatRepository;
        }

        public Task<IEnumerable<Format>> GetAsync() => _formatRepository.GetAsync();
    }
}

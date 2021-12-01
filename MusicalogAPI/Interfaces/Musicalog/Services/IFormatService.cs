using MusicalogAPI.Models.Musicalog;

namespace MusicalogAPI.Interfaces.Musicalog.Services
{
    public interface IFormatService
    {
        Task<IEnumerable<Format>> GetAsync();
    }
}

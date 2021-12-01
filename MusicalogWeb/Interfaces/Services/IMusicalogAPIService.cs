using MusicalogWeb.Models.MusicalogAPI;

namespace MusicalogWeb.Interfaces.Services
{
    public interface IMusicalogAPIService
    {
        Task<List<Format>?> GetFormatsAsync();

        Task<List<Album>?> GetAlbumsAsync();
        Task<List<Album>?> GetAlbumsAsync(Dictionary<string, string> filters);
        Task<Album?> GetAlbumAsync(Guid Id);
        Task<Album?> CreateAlbumAsync(Album album);
        Task<Album?> UpdateAlbumAsync(Album album);
        Task<Album?> DeleteAlbumAsync(Guid Id);

        Task<List<Artist>?> GetArtistsAsync();
        Task<Artist?> GetArtistAsync(Guid Id);
        Task<Artist?> CreateArtistAsync(Artist artist);
        Task<Artist?> UpdateArtistAsync(Artist artist);
        Task<Artist?> DeleteArtistAsync(Guid Id);
    }
}

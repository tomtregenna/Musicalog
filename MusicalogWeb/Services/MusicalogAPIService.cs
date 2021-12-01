using MusicalogWeb.Models.MusicalogAPI;
using MusicalogWeb.Interfaces.Services;
using System.Text.Json;
using System.Text;
using System.Net.Mime;

namespace MusicalogWeb.Services
{
    public class MusicalogAPIService : IMusicalogAPIService
    {
        private string MusicalogAPI { get; init; }

        public MusicalogAPIService(GlobalOptions globals)
        {
            MusicalogAPI = globals.MusicalogAPI;
        }

        public async Task<List<Format>?> GetFormatsAsync()
        {
            var url = $"{MusicalogAPI}formats";

            using var client = new HttpClient();

            var response = await client.GetAsync(url);
            var formats = await response.Content.ReadFromJsonAsync<List<Format>?>();

            return formats;
        }

        public async Task<List<Album>?> GetAlbumsAsync(Dictionary<string, string> filters)
        {
            var url = $"{MusicalogAPI}albums";

            var filtersJson = JsonSerializer.Serialize(filters);
            var apiResponse = String.Empty;

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(url),
                    Content = new StringContent(filtersJson, Encoding.UTF8, MediaTypeNames.Application.Json),
                };

                var response = await client.SendAsync(request).ConfigureAwait(false);

                var albums = await response.Content.ReadFromJsonAsync<List<Album>?>();

                return albums;
            }
        }
        public async Task<List<Album>?> GetAlbumsAsync()
        {
            var url = $"{MusicalogAPI}albums";

            using var client = new HttpClient();

            var response = await client.GetAsync(url);
            var albums = await response.Content.ReadFromJsonAsync<List<Album>?>();

            return albums;
        }
        public async Task<Album?> GetAlbumAsync(Guid Id)
        {
            var url = $"{MusicalogAPI}albums/{Id}";

            using var client = new HttpClient();

            var response = await client.GetAsync(url);
            var album = await response.Content.ReadFromJsonAsync<Album>();

            return album;
        }
        public async Task<Album?> CreateAlbumAsync(Album album)
        {
            var url = $"{MusicalogAPI}albums";

            var jsonAlbum = JsonSerializer.Serialize(album);

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(url),
                    Content = new StringContent(jsonAlbum, Encoding.UTF8, MediaTypeNames.Application.Json)
                };

                var response = await client.SendAsync(request).ConfigureAwait(false);
                var newAlbum = await response.Content.ReadFromJsonAsync<Album?>();

                return newAlbum;
            }
        }
        public async Task<Album?> UpdateAlbumAsync(Album album)
        {
            var url = $"{MusicalogAPI}albums";

            var jsonAlbum = JsonSerializer.Serialize(album);

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri(url),
                    Content = new StringContent(jsonAlbum, Encoding.UTF8, MediaTypeNames.Application.Json)
                };

                var response = await client.SendAsync(request).ConfigureAwait(false);
                var newAlbum = await response.Content.ReadFromJsonAsync<Album?>();

                return newAlbum;
            }
        }
        public async Task<Album?> DeleteAlbumAsync(Guid Id)
        {
            var url = $"{MusicalogAPI}albums/{Id}";

            using var client = new HttpClient();

            var response = await client.DeleteAsync(url);
            var deletedAlbum = await response.Content.ReadFromJsonAsync<Album>();

            return deletedAlbum;
        }

        public async Task<List<Artist>?> GetArtistsAsync()
        {
            var url = $"{MusicalogAPI}artists";

            using var client = new HttpClient();

            var response = await client.GetAsync(url);
            var artists = await response.Content.ReadFromJsonAsync<List<Artist>?>();

            return artists;
        }
        public async Task<Artist?> GetArtistAsync(Guid Id)
        {
            var url = $"{MusicalogAPI}artists/{Id}";

            using var client = new HttpClient();

            var response = await client.GetAsync(url);
            var artist = await response.Content.ReadFromJsonAsync<Artist>();

            return artist;
        }
        public async Task<Artist?> CreateArtistAsync(Artist artist)
        {
            var url = $"{MusicalogAPI}artists";

            var jsonArtist = JsonSerializer.Serialize(artist);

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(url),
                    Content = new StringContent(jsonArtist, Encoding.UTF8, MediaTypeNames.Application.Json)
                };

                var response = await client.SendAsync(request).ConfigureAwait(false);
                var newArtist = await response.Content.ReadFromJsonAsync<Artist?>();

                return newArtist;
            }
        }
        public async Task<Artist?> UpdateArtistAsync(Artist artist)
        {
            var url = $"{MusicalogAPI}artists";

            var jsonArtist = JsonSerializer.Serialize(artist);

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri(url),
                    Content = new StringContent(jsonArtist, Encoding.UTF8, MediaTypeNames.Application.Json)
                };

                var response = await client.SendAsync(request).ConfigureAwait(false);
                var newArtist = await response.Content.ReadFromJsonAsync<Artist?>();

                return newArtist;
            }
        }
        public async Task<Artist?> DeleteArtistAsync(Guid Id)
        {
            var url = $"{MusicalogAPI}artists/{Id}";

            using var client = new HttpClient();

            var response = await client.DeleteAsync(url);
            var deletedArtist = await response.Content.ReadFromJsonAsync<Artist>();

            return deletedArtist;
        }
    }
}

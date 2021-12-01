using MusicalogWeb.Models.MusicalogAPI;

namespace MusicalogWeb.ViewModels
{
    public class ArtistsViewModel
    {
        public List<Artist> Artists { get; set; }
        public List<Format> Formats { get; set; }

        public Artist NewArtist { get; set; }
    }
}
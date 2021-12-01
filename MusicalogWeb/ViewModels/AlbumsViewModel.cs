using MusicalogWeb.Models.MusicalogAPI;

namespace MusicalogWeb.ViewModels
{
    public class AlbumsViewModel
    {
        public List<Album> Albums { get; set; }
        public List<Artist> Artists { get; set; }
        public List<Format> Formats { get; set; }

        public Album NewAlbum { get; set; }
        public Album UpdatedAlbum { get; set; }
        public Album RemovedAlbum { get; set; }

        public bool RedirectFromAction { get; set; } = false;
        public string ErrorMsg { get; set; }
    }
}

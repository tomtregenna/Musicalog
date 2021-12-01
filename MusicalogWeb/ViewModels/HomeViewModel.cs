using MusicalogWeb.Models.MusicalogAPI;

namespace MusicalogWeb.ViewModels
{
    public class HomeViewModel
    {
        public string FilterType { get; set; }
        public List<string> Errors { get; set; }
        public List<Album> Albums { get; set; }
        public List<Artist> Artists { get; set; }
        public List<Format> Formats { get; set; }
    }
}

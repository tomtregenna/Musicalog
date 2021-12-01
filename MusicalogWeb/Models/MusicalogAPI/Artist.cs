namespace MusicalogWeb.Models.MusicalogAPI
{
    public partial class Artist
    {
        public Artist()
        {
            Albums = new HashSet<Album>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Album> Albums { get; set; }
    }
}

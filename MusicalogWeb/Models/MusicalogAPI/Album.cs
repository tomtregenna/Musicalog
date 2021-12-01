namespace MusicalogWeb.Models.MusicalogAPI
{
    public partial class Album
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public Guid ArtistId { get; set; }
        public int FormatId { get; set; }
        public int Stock { get; set; }

        public virtual Artist Artist { get; set; } = null!;
        public virtual Format FormatIdNavigation { get; set; } = null!;
    }
}

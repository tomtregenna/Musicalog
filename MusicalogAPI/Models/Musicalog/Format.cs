namespace MusicalogAPI.Models.Musicalog
{
    public partial class Format
    {
        public Format()
        {
            Albums = new HashSet<Album>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Album> Albums { get; set; }
    }
}

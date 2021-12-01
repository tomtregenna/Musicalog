using Microsoft.EntityFrameworkCore;

namespace MusicalogAPI.Models.Musicalog
{
    public partial class MusicalogContext : DbContext
    {
        public MusicalogContext()
        {
        }

        public MusicalogContext(DbContextOptions<MusicalogContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Album> Albums { get; set; } = null!;
        public virtual DbSet<Artist> Artists { get; set; } = null!;
        public virtual DbSet<Format> Formats { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=SQL-VM1;Initial Catalog=Musicalog;Persist Security Info=True;User ID=MusicalogUser;Password=password");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("db_owner");

            modelBuilder.Entity<Album>(entity =>
            {
                entity.ToTable("Album", "dbo");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ArtistId).HasColumnName("ArtistID");

                entity.Property(e => e.Title).IsUnicode(false);

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.Albums)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientCascade)
                    .HasConstraintName("FK_Album_Artist");

                entity.HasOne(d => d.FormatIdNavigation)
                    .WithMany(p => p.Albums)
                    .HasForeignKey(d => d.FormatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Album_Album");
            });

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.ToTable("Artist", "dbo");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Format>(entity =>
            {
                entity.ToTable("Format", "dbo");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

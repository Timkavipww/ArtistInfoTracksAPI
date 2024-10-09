using ArtistInfoTracksAPI.Models.ArtistsModel;
using ArtistInfoTracksAPI.Models.TrackModel;
using Microsoft.EntityFrameworkCore;

namespace ArtistInfoTracksAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Artist> Artists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>()
                .HasKey(key => key.Id);
            modelBuilder.Entity<Track>()
                .HasKey(key => key.ArtistId);

            

            modelBuilder.Entity<Artist>()
                .HasMany(a => a.Tracks)
                .WithOne(t => t.Artist)
                .HasForeignKey(t => t.ArtistId);


            base.OnModelCreating(modelBuilder);
        }
    }
}


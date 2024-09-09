using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Music.Domain.Entities.AlbumAgg;
using Music.Domain.Entities.ArtistAgg;
using Music.Domain.Entities.BandAgg;
using Music.Domain.Entities.GenreAgg;
using Music.Domain.Entities.PictureAgg;
using Music.Domain.Entities.TrackAgg;
using Music.EfCore.Mapping;

namespace Music.EfCore;

public class MusicDbContext : DbContext
{
    public MusicDbContext(DbContextOptions<MusicDbContext> context) : base(context)
    {

    }


    public DbSet<AlbumEntity> AlbumEntities { get; set; }
    public DbSet<ArtistEntity> ArtistEntities { get; set; }
    public DbSet<InstrumentEntity> InstrumentEntities { get; set; }
    public DbSet<BandEntity> BandEntities { get; private set; }
    public DbSet<GenreEntity> GenreEntities { get; set; }
    public DbSet<ArtistBandEntity> ArtistBandEntities { get; set; }
    public DbSet<TrackEntity> TrackEntities { get; set; }
    public DbSet<PictureBase> PictureBases { get; set; }
    public DbSet<ArtistPicture> ArtistPictures { get; set; }
    public DbSet<BandPicture> BandPictures { get; set; }
    public DbSet<AlbumPicture> AlbumPictures { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AlbumEntity>().ToTable("tbAlbums");
        modelBuilder.Entity<ArtistEntity>().ToTable("tbArtists");
        modelBuilder.Entity<InstrumentEntity>().ToTable("tbInstruments");
        modelBuilder.Entity<BandEntity>().ToTable("tbBands");
        modelBuilder.Entity<GenreEntity>().ToTable("tbGenres");
        modelBuilder.Entity<TrackEntity>().ToTable("tbTracks");
        modelBuilder.Entity<PictureBase>().ToTable("tbPictures");
        modelBuilder.Entity<ArtistBandEntity>().ToTable("tbArtistBands");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BandMapping).Assembly);




        modelBuilder.Entity<ArtistBandEntity>().Ignore(e => e.State);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().
                     SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entity.GetProperties().Where(p => p.IsPrimaryKey()))
            {
                property.ValueGenerated = ValueGenerated.Never;
            }
        }

    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Music.Domain.Entities.AlbumAgg;

namespace Music.EfCore.Mapping;

public class AlbumMapping : IEntityTypeConfiguration<AlbumEntity>
{
    public void Configure(EntityTypeBuilder<AlbumEntity> builder)
    {
        builder.HasMany(e => e.Genres)
            .WithMany(e => e.Albums);

        builder.HasMany(e => e.Covers)
            .WithOne(e => e.Album)
            .HasForeignKey(e => e.AlbumId);

        builder.HasMany(e => e.Tracks)
            .WithOne(e => e.Album)
            .HasForeignKey(e => e.AlbumId);


        builder.HasMany(e => e.Genres)
            .WithMany(e => e.Albums)
            .UsingEntity("tbAlbumsGenres");

    }
}
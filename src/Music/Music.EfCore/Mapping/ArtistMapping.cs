using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Music.Domain.Entities.ArtistAgg;

namespace Music.EfCore.Mapping;

public class ArtistMapping : IEntityTypeConfiguration<ArtistEntity>
{
    public void Configure(EntityTypeBuilder<ArtistEntity> builder)
    {

        builder.HasMany(e => e.Artists)
            .WithOne(e => e.Artist)
            .HasForeignKey(e => e.ArtistId);


        builder.HasMany(e => e.Pictures)
            .WithOne(e => e.Artist)
            .HasForeignKey(e => e.ArtistId);


        builder.HasMany(e => e.Instruments)
            .WithMany(e => e.Artist)
            .UsingEntity("tbArtistsInstruments");

    }
}
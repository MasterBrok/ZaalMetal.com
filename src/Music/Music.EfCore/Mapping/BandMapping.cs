using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Music.Domain.Entities.BandAgg;

namespace Music.EfCore.Mapping;

public class BandMapping : IEntityTypeConfiguration<BandEntity>
{
    public void Configure(EntityTypeBuilder<BandEntity> builder)
    {
        builder.HasMany(e => e.Artists)
            .WithOne(e => e.Band)
            .HasForeignKey(e => e.BandId);


        builder.HasMany(e => e.Genres)
            .WithMany(e => e.Bands)
            .UsingEntity("tbBandsGenres");

    }
}
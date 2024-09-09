using Framework.Infrastructure;
using Music.Domain.Entities.BandAgg;

namespace Music.EfCore.Repositories;

public class ArtistBandRepository : RepositoryBase<ArtistBandEntity>, IArtistBandRepository
{
    public ArtistBandRepository(MusicDbContext context) : base(context)
    {
    }
}
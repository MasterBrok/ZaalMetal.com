using Framework.Infrastructure;
using Music.Domain.Entities.ArtistAgg;

namespace Music.EfCore.Repositories;

public class ArtistRepository : RepositoryBase<ArtistEntity>, IArtistRepository
{
    public ArtistRepository(MusicDbContext context) : base(context)
    {
    }
}
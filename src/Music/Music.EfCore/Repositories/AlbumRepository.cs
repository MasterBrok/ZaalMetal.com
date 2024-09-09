using Framework.Infrastructure;
using Music.Domain.Entities.AlbumAgg;

namespace Music.EfCore.Repositories;

public class AlbumRepository : RepositoryBase<AlbumEntity>, IAlbumRepository
{
    public AlbumRepository(MusicDbContext context) : base(context)
    {
    }
}
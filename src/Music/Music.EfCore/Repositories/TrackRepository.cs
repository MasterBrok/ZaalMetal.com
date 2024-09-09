using Framework.Infrastructure;
using Music.Domain.Entities.TrackAgg;

namespace Music.EfCore.Repositories;

public class TrackRepository : RepositoryBase<TrackEntity> , ITrackRepository
{
    public TrackRepository(MusicDbContext context) : base(context)
    {
    }
}
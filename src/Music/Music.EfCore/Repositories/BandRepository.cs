using Framework.Infrastructure;
using Music.Domain.Entities.BandAgg;

namespace Music.EfCore.Repositories;

public class BandRepository : RepositoryBase<BandEntity>, IBandRepository
{
    public BandRepository(MusicDbContext context) : base(context)
    {
    }
}
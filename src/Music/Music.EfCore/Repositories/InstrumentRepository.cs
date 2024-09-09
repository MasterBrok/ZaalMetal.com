using Framework.Infrastructure;
using Music.Domain.Entities.ArtistAgg;

namespace Music.EfCore.Repositories;

public class InstrumentRepository : RepositoryBase<InstrumentEntity>, IInstrumentRepository
{
    public InstrumentRepository(MusicDbContext context) : base(context)
    {
    }
}
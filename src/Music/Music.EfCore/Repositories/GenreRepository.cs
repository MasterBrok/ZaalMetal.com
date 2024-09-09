using Framework.Infrastructure;
using Music.Domain.Entities.GenreAgg;

namespace Music.EfCore.Repositories;

public class GenreRepository : RepositoryBase<GenreEntity>, IGenreRepository
{
    public GenreRepository(MusicDbContext context) : base(context)
    {
    }
}
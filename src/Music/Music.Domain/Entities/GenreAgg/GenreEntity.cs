using Framework.Domain;
using Music.Domain.Entities.AlbumAgg;
using Music.Domain.Entities.BandAgg;

namespace Music.Domain.Entities.GenreAgg;

public class GenreEntity : EntityBase
{
    public GenreEntity(string title, string? description)
    {
        Title = title;
        Description = description;
    }

    public void Edit(string title, string? description)
    {
        Title = title;
        Description = description;
    }

    public string Title { get; private set; }
    public string? Description { get; private set; }


    public ICollection<BandEntity>? Bands { get; private set; }
    public ICollection<AlbumEntity>? Albums { get; private set; }

}
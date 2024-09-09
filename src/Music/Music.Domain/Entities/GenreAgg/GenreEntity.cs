using Framework.Domain;
using Music.Domain.Entities.AlbumAgg;
using Music.Domain.Entities.BandAgg;

namespace Music.Domain.Entities.GenreAgg;

public class GenreEntity : EntityBase
{
    public GenreEntity(string title)
    {
        Title = title;
    }

    public string Title { get; set; }
    public string? Description { get; set; }


    public ICollection<BandEntity>? Bands { get; private set; }
    public ICollection<AlbumEntity>? Albums { get; private set; }

}
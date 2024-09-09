using Framework.Domain;

namespace Music.Domain.Entities.ArtistAgg;

public class InstrumentEntity : EntityBase
{
    public InstrumentEntity(string title)
    {
        Title = title;
    }

    public string Title { get; set; }

    public ICollection<ArtistEntity> Artist { get; private set; }

}
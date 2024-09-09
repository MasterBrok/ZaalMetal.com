using Framework.Domain;
using Music.Domain.Entities.BandAgg;

namespace Music.Domain.Entities.ArtistAgg;

public class InstrumentEntity : EntityBase
{
    protected InstrumentEntity()
    {
        
    }
    public InstrumentEntity(string title)
    {
        Title = title;
    }

    public void Edit(string title)
    {
        Title = title;
    }

    public string Title { get; set; }
    public ICollection<ArtistEntity> Artist { get; private set; }
    public ICollection<BandEntity> Bands { get; private set; }

}
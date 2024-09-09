using Framework.Domain;
using Music.Domain.Entities.ArtistAgg;

namespace Music.Domain.Entities.BandAgg;

public class ArtistBandEntity : EntityBase
{
    public ArtistBandEntity(string artistId, string bandId, string? description)
    {
        ArtistId = artistId;
        BandId = bandId;
        Description = description;

        ArtistInBandState = ArtistInBandState.Join;
    }

    public ArtistEntity Artist { get; private set; }
    public string ArtistId { get; private set; }


    public BandEntity Band { get; private set; }
    
    public string BandId { get; private set; }
    public string? Description { get; private set; }
    
    public ArtistInBandState ArtistInBandState { get; private set; }

    public ICollection<InstrumentEntity> Roles { get; private set; }

    public void AddRole(InstrumentEntity model) => Roles.Add(model);
    public void RemoveRole(InstrumentEntity model) => Roles.Remove(model);
}
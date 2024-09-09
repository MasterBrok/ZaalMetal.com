using Framework.Domain;
using Music.Domain.Entities.BandAgg;
using Music.Domain.Entities.PictureAgg;

namespace Music.Domain.Entities.ArtistAgg;

public class ArtistEntity : EntityBase
{
    public ArtistEntity(string firstName, string lastName, string? nickName, string? bio, DateTime dateOfBirth, DateTime? dateOfDeath)
    {
        FirstName = firstName;
        LastName = lastName;
        NickName = nickName;
        Bio = bio;
        DateOfBirth = dateOfBirth;
        DateOfDeath = dateOfDeath;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? NickName { get; set; }
    public string? Bio { get; private set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime? DateOfDeath { get; set; }

    public string Tag { get; private set; }
    public ICollection<InstrumentEntity> Instruments { get; private set; }
    public ICollection<ArtistBandEntity> Artists { get; private set; }
    public ICollection<ArtistPicture> Pictures { get; private set; }




}
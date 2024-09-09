using Framework.Domain;
using Music.Domain.Entities.BandAgg;
using Music.Domain.Entities.PictureAgg;

namespace Music.Domain.Entities.ArtistAgg;

public class ArtistEntity : EntityBase
{
    protected ArtistEntity() { }
    public ArtistEntity(string firstName, string lastName, string? nickName, string? bio, DateTime dateOfBirth, DateTime? dateOfDeath, string slug)
    {
        FirstName = firstName;
        LastName = lastName;
        NickName = nickName;
        Bio = bio;
        DateOfBirth = dateOfBirth;
        DateOfDeath = dateOfDeath;
        Slug = slug;
    }

    public void Edit(string firstName, string lastName, string? nickName, string? bio, DateTime dateOfBirth, DateTime? dateOfDeath)
    {
        FirstName = firstName;
        LastName = lastName;
        NickName = nickName;
        Bio = bio;
        DateOfBirth = dateOfBirth;
        DateOfDeath = dateOfDeath;
    }


    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string? NickName { get; private set; }
    public string? Bio { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public DateTime? DateOfDeath { get; private set; }

    public string Slug { get; private set; }
    public ICollection<InstrumentEntity>? Instruments { get; private set; }
    public ICollection<ArtistBandEntity>? Artists { get; private set; }
    public ICollection<ArtistPicture>? Pictures { get; private set; }

    public string FullName => LastName + FirstName;
}
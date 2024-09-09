using Music.Domain.Entities.ArtistAgg;

namespace Music.Domain.Entities.PictureAgg;

public class ArtistPicture : PictureBase
{
    public string ArtistId { get; private set; }
    public ArtistEntity Artist{ get; set; }

    public ArtistPicture(string path, string artistId) : base(path)
    {
        ArtistId = artistId;
    }
}
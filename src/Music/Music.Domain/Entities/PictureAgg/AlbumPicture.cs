using Music.Domain.Entities.AlbumAgg;

namespace Music.Domain.Entities.PictureAgg;

public class AlbumPicture : PictureBase
{
    public AlbumPicture(string path, string albumId) : base(path)
    {
        AlbumId = albumId;
    }

    public AlbumEntity Album { get; private set; }
    public string AlbumId { get; private set; }
}
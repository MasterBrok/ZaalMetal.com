using Framework.Domain;
using Music.Domain.Entities.BandAgg;
using Music.Domain.Entities.GenreAgg;
using Music.Domain.Entities.PictureAgg;

namespace Music.Domain.Entities.AlbumAgg;

public class AlbumEntity : EntityBase
{
    public AlbumEntity(string title, string? description, DateTime publishTimeAt, string path, string bandId)
    {
        Title = title;
        Description = description;
        PublishTimeAt = publishTimeAt;
        Path = path;
        BandId = bandId;
    }

    public string Title { get; private set; }
    public string? Description { get; private set; }
    public DateTime PublishTimeAt { get; private set; }
    public string Path { get; private set; }
    public string Tag { get; private set; }


    public BandEntity Band { get; private set; }
    public string BandId { get; private set; }

    public ICollection<AlbumPicture>? Pictures { get; private set; }

    public ICollection<GenreEntity>? Genres { get; private set; }

}

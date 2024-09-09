using Framework.Domain;
using Music.Domain.Entities.BandAgg;
using Music.Domain.Entities.GenreAgg;
using Music.Domain.Entities.PictureAgg;
using Music.Domain.Entities.TrackAgg;

namespace Music.Domain.Entities.AlbumAgg;

public class AlbumEntity : EntityBase
{
    protected AlbumEntity()
    {

    }
    public AlbumEntity(string name, string? description, DateTime publishTimeAt, string bandId, string slug, string tag)
    {
        Name = name;
        Description = description;
        PublishTimeAt = publishTimeAt;
        BandId = bandId;
        Slug = slug;
        Tag = tag;
    }

    public void Edit(string name, string? description, DateTime publishTimeAt, string bandId)
    {
        Name = name;
        Description = description;
        PublishTimeAt = publishTimeAt;
        BandId = bandId;
    }



    public string Name { get; private set; }
    public string Slug { get; private set; }
    public string Folder { get; private set; }
    public string? Description { get; private set; }
    public DateTime PublishTimeAt { get; private set; }
    public string Tag { get; private set; }


    public BandEntity Band { get; private set; }
    public string BandId { get; private set; }

    public ICollection<AlbumPicture>? Covers { get; private set; }
    public ICollection<TrackEntity> Tracks { get; private set; }
    public ICollection<GenreEntity>? Genres { get; private set; }


    public void CreatePath()
    {
        Folder = $"{Name}-{BandId}";
    }

}

using Framework.Domain;
using Music.Domain.Entities.AlbumAgg;

namespace Music.Domain.Entities.TrackAgg;
public class TrackEntity : EntityBase
{
    protected TrackEntity()
    {
    }
    public TrackEntity(string name, string musicPath, TimeSpan duration, string albumId, string? description)
    {
        Name = name;
        MusicPath = musicPath;
        Duration = duration;
        AlbumId = albumId;
        Description = description;
    }

    public void Edit(string name, string path, TimeSpan duration, string albumId, string? description)
    {
        Name = name;
        MusicPath = path;
        Duration = duration;
        Description = description;
        AlbumId = albumId;
    }


    public string Name { get; private set; }
    public string MusicPath { get; private set; }
    public TimeSpan Duration { get; private set; }
    public string? Description { get; private set; }

    public string AlbumId { get; private set; }
    public AlbumEntity Album { get; private set; }

}
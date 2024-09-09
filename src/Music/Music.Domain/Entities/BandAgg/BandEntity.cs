using Framework.Domain;
using Microsoft.IdentityModel.Tokens;
using Music.Domain.Entities.AlbumAgg;
using Music.Domain.Entities.GenreAgg;
using Music.Domain.Entities.PictureAgg;

namespace Music.Domain.Entities.BandAgg;

public class BandEntity : EntityBase
{
    public BandEntity(string title, string bio, DateTime? formationDate, DateTime? dissolvedTimeAt, string tag, string slug, string icon)
    {
        Title = title;
        Bio = bio;
        FormationDate = formationDate;
        DissolvedTimeAt = dissolvedTimeAt;
        Tag = tag;
        Slug = slug;
        Icon = icon;
    }

    public void Edit(string title, string bio, DateTime? formationDate, DateTime? dissolvedTimeAt, string icon)
    {
        Title = title;
        Bio = bio;
        FormationDate = formationDate;
        DissolvedTimeAt = dissolvedTimeAt;
        if (icon.IsNullOrEmpty())
            Icon = icon;
    }



    public string Title { get; private set; }
    public string Bio { get; private set; }
    public DateTime? DissolvedTimeAt { get; private set; }
    public DateTime? FormationDate { get; private set; }
    public string Tag { get; private set; }
    public string Slug { get; private set; }
    public string Icon { get; private set; }

    public ICollection<AlbumEntity> Albums { get; private set; }
    public ICollection<GenreEntity> Genres { get; private set; }
    public ICollection<ArtistBandEntity> Artists { get; private set; }
    public ICollection<BandPicture> Pictures { get; private set; }




    // Artist

    public bool HadArtist(string artistId) => Artists.Any(e => e.ArtistId == artistId);

    public void JoinArtist(string artistId)
    {
        var model = new ArtistBandEntity(artistId, this.Id, string.Empty);
        Artists.Add(model);
    }

    public void RemoveArtist(string artistId, string description)
    {
        var model = new ArtistBandEntity(artistId, this.Id, description);
        Artists.Remove(model);
    }


    // Genre
    public bool IsDefineGenre(string genreId) => Genres.Any(e => e.Id == genreId);

    public void AddGenre(GenreEntity genre)
    {
        Genres.Add(genre);
    }

    public void RemoveGenre(GenreEntity genre)
    {
        Genres.Remove(genre);
    }
}
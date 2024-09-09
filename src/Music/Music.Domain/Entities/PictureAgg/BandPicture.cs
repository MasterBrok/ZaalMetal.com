using Music.Domain.Entities.BandAgg;

namespace Music.Domain.Entities.PictureAgg;

public class BandPicture : PictureBase
{
    public string BandId { get; private set; }
    public BandEntity Band { get; set; }

    public BandPicture(string path, string bandId) : base(path)
    {
        BandId = bandId;
    }

}
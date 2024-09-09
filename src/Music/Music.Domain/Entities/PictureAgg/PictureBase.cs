using Framework.Domain;

namespace Music.Domain.Entities.PictureAgg;

 public abstract class PictureBase : EntityBase
{
    protected PictureBase(string path)
    {
        Path = path;
    }

    public string Path { get; private set; }

}
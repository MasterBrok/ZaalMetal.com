using Microsoft.AspNetCore.Http;

namespace Framework.Application;

public interface IFileUpload
{
    Task<PathFolder> Create(string name);
    Task<SubAlbum> CreateSubAlbum(string band, string album);
}
public record PathFolder
{
    public string Band { get; set; }
}

public record SubAlbum
{
    public string Name { get; set; }
}

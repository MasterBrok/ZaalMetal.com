using System.Buffers;
using Framework.Application;

namespace ZaalMetal.API.Services;

public interface IFileUpload
{
    Task<PathFolder> CreateBand(string name);
    Task<PathFolder> CreateArtist(string name);
    Task<SubAlbum> CreateSubAlbum(string band, string album);
    Task UploadCover(IFormFileCollection covers, string bandName, string albumName);
    Task<bool> AnyArtist(string? artist);
    Task<List<string>> UploadArtist(IFormFileCollection files, string artist);
}

public class FileUpload : IFileUpload
{
    private readonly IWebHostEnvironment _env;

    private const string Albums = "Albums";
    private const string Covers = "Covers";
    private const string Artists = "Artists";

    public FileUpload(IWebHostEnvironment environment)
    {
        _env = environment;
    }


    public async Task<PathFolder> CreateBand(string name)
    {
        var path = Path.Combine(_env.WebRootPath, name);

        var pt = new PathFolder();

        if (Directory.Exists(path))
            return pt;


        // Create Band Folder
        pt.Path = Directory.CreateDirectory(path).Name;

        // Create Album Folder
        Directory.CreateDirectory(Path.Combine(path, Albums));
        return pt;
    }

    public async Task<PathFolder> CreateArtist(string name)
    {
        var path = Path.Combine(_env.WebRootPath, Artists, name);

        var pt = new PathFolder();

        if (Directory.Exists(path))
            return pt;

        // Create Artist Folder
        pt.Path = Directory.CreateDirectory(path).Name;
        return pt;
    }

    public async Task<SubAlbum> CreateSubAlbum(string band, string album)
    {
        var sub = new SubAlbum();
        var path = Path.Combine(_env.WebRootPath, band);

        if (!Directory.Exists(path))
            return null;

        path = Path.Combine(path, Albums, album);

        // Create Album Folder
        sub.Name = Directory.CreateDirectory(path).Name;

        // Create Cover Folder
        Directory.CreateDirectory(Path.Combine(path, Covers));

        return sub;
    }



    // Band Name = Band-Tag
    // Album Name = Album-Year
    public async Task UploadCover(IFormFileCollection covers, string bandName, string albumName)
    {
        string path = Path.Combine(_env.ContentRootPath, bandName, albumName);

        foreach (var cover in covers)
        {
            var create = File.Create(Path.Combine(path, cover.FileName));
            await cover.CopyToAsync(create);
        }
    }

    public async Task<bool> AnyArtist(string? artist)
    {
        return Directory.Exists(Path.Combine(_env.WebRootPath,Artists,artist));
    }

    public async Task<List<string>> UploadArtist(IFormFileCollection files, string artist)
    {
        try
        {
            var list = new List<string>();
            string path = Path.Combine(_env.WebRootPath,Artists,artist);
            foreach (var file in files)
            {
                await using var re = File.Create(path + "\\" + file.FileName);
                await file.CopyToAsync(re);
                list.Add(re.Name);
            }

            return list;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
}

public record PathFolder
{
    public string Path { get; set; }
}

public record SubAlbum
{
    public string Name { get; set; }
}

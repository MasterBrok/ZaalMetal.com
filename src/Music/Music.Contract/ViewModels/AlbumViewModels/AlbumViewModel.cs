namespace Music.Contract.ViewModels.AlbumViewModels;

public record AlbumViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string CreateTimeAt { get; set; }
    public string Cover { get; set; }
}
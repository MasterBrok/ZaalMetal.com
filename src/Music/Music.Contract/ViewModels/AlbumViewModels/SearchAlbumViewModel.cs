namespace Music.Contract.ViewModels.AlbumViewModels;

public record SearchAlbumViewModel
{
    public string? Name { get; set; }
    public string? BandId { get; set; }
    public string? Tag { get; set; }
}
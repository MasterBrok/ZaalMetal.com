namespace Music.Contract.ViewModels.AlbumViewModels;

public record EditAlbumViewModel : AddAlbumViewModel
{
    public string? Id { get; set; }
}
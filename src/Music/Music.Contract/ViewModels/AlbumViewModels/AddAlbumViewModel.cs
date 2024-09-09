namespace Music.Contract.ViewModels.AlbumViewModels;

public record AddAlbumViewModel
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime PublishTimeAt { get; set; }
    public string BandId { get; set; }
}
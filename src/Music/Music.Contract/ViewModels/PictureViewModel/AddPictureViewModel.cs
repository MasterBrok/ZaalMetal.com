namespace Music.Contract.ViewModels.PictureViewModel;

public record AddPictureViewModel
{
    public string? ItemId { get; set; }
    public List<string>? Paths { get; set; }
}
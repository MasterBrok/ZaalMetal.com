namespace Music.Contract.ViewModels.ArtistViewModel;

public record EditArtistViewModel : AddArtistViewModel
{
    public string Id { get; set; }
}
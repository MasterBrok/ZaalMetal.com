namespace Music.Contract.ViewModels.ArtistViewModel;

public record SearchArtistViewModel
{
    public string? Id { get; set; }
    public string? LastName { get; set; }
    public string? FistName { get; set; }
}
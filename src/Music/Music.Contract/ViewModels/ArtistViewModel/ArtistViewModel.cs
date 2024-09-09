using Framework.Domain;

namespace Music.Contract.ViewModels.ArtistViewModel;

public record ArtistViewModel
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public string Image { get; set; }
    public State State { get; set; }
}
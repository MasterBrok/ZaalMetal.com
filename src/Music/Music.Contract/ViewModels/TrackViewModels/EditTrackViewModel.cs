using Music.Contract.ViewModels.AlbumViewModels;

namespace Music.Contract.ViewModels.TrackViewModels;

public record EditTrackViewModel : NewTrackViewModel
{
    public string Id { get; set; }
}

using Microsoft.AspNetCore.Http;

namespace Music.Contract.ViewModels.TrackViewModels;

public record NewTrackViewModel
{
    public string Name { get;  set; }
    public IFormFile Path { get;  set; }
    public TimeSpan Duration { get; set; }
    public string? Description { get;  set; }
    public string AlbumId { get;  set; }
}
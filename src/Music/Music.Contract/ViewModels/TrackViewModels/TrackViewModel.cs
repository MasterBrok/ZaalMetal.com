namespace Music.Contract.ViewModels.TrackViewModels;

public record TrackViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public TimeSpan Duration { get; set; }
    public string Cover { get; set; }
    public string Path { get; set; }
}
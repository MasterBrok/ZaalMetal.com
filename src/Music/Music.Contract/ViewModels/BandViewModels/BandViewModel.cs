namespace Music.Contract.ViewModels.BandViewModels;

public record BandViewModel
{
    public string Id { get; set; }
    public string Tag { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public string CreateTimeAt { get; set; }
}
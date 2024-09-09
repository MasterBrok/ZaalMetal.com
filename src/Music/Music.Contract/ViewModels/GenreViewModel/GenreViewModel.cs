namespace Music.Contract.ViewModels.GenreViewModel;

public record GenreViewModel
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
}
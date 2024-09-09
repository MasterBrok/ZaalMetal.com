namespace Music.Contract.ViewModels.GenreViewModel;

public record AddGenreViewModel
{
    public string Title { get; set; }
    public string? Description { get; set; }
}
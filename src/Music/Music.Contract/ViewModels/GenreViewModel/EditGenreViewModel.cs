namespace Music.Contract.ViewModels.GenreViewModel;

public record EditGenreViewModel : AddGenreViewModel
{
    public string Id { get; set; }
}
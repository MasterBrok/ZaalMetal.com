namespace Music.Contract.ViewModels.BandViewModels;

public record EditBandViewModel : NewBandViewModel
{
    public string Id { get; set; }
}
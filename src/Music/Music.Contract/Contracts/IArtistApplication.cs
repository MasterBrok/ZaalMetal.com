using Framework.Application;
using Music.Contract.ViewModels.ArtistViewModel;

namespace Music.Contract.Contracts;

public interface IArtistApplication : IApplication<AddArtistViewModel, EditArtistViewModel, SearchArtistViewModel, ArtistViewModel>
{
    Task<OperationResult<string>> GetFullName(string id);
}
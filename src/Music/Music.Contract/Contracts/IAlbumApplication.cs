using Framework.Application;
using Music.Contract.ViewModels.AlbumViewModels;

namespace Music.Contract.Contracts;

public interface IAlbumApplication : IApplication<AddAlbumViewModel, EditAlbumViewModel, SearchAlbumViewModel, AlbumViewModel>
{

}
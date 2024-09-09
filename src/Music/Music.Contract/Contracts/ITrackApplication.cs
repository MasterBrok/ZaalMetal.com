using Framework.Application;
using Music.Contract.ViewModels.TrackViewModels;

namespace Music.Contract.Contracts;

public interface ITrackApplication : IApplication<NewTrackViewModel, EditTrackViewModel, SearchTrackViewModel, TrackViewModel>
{

}
using Framework.Application;
using Music.Contract.ViewModels.BandViewModels;

namespace Music.Contract.Contracts;

public interface IBandApplication : IApplication<NewBandViewModel,EditBandViewModel,SearchBandViewModel,BandViewModel>
{
    
}
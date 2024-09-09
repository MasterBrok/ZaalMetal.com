using Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Music.Contract.Contracts;
using Music.Contract.ViewModels.BandViewModels;
using ZaalMetal.API.Services;

namespace ZaalMetal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BandController : ControllerBase
    {

        private readonly IBandApplication _bandApplication;
        private CancellationToken cancellationToken = new();
        public BandController(IBandApplication bandApplication)
        {
            _bandApplication = bandApplication;
        }


        [HttpPost("NewBand")]
        public async Task<BaseResult> Add([FromBody] NewBandViewModel band)
        {
            return await _bandApplication.Add(band);
        }

        [HttpPut("UpdateBand")]
        public async Task<BaseResult> Update([FromBody] EditBandViewModel band)
        {
            return await _bandApplication.Update(band);
        }

        [HttpGet("GetDetail")]
        public async Task<ApiResponse<EditBandViewModel>> GetDetail(string id)
        {
            return (ApiResponse<EditBandViewModel>)await _bandApplication.GetDetail(new SearchBandViewModel()
            {
                Id = id
            });
        }

        [HttpGet("Bands")]
        public async Task<ApiResponse<List<BandViewModel>>> Bands([FromQuery]SearchBandViewModel search, [FromQuery] PaginationParameters parameters)
        {
            return (ApiResponse<List<BandViewModel>>)await _bandApplication.ToViews(search, parameters, cancellationToken);
        }
    }
}

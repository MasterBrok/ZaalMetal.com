using System.Net;
using Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Music.Contract.Contracts;
using Music.Contract.ViewModels.ArtistViewModel;
using ZaalMetal.API.Services;
using IFileUpload = ZaalMetal.API.Services.IFileUpload;

namespace ZaalMetal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {

        private readonly IFileUpload _fileUpload;

        private readonly IArtistApplication _artistApplication;

        private CancellationToken cancellationToken = new();
        public ArtistController(IArtistApplication artistApplication, IFileUpload fileUpload)
        {
            _artistApplication = artistApplication;
            _fileUpload = fileUpload;
        }

        [HttpPost("UploadCovers")]
        public async Task<BaseResult> Uploads(IFormFileCollection files, string artistId)
        {
            var op = new BaseResult();

            var find = await _artistApplication.GetFullName(artistId);

            if (!find.Success)
                return op.NotFound();

            if (!await _fileUpload.AnyArtist(find.Data))
                return op.NotFound();

            var upload = await _fileUpload.UploadArtist(files, find.Data);

        }


        [HttpPost("NewArtist")]
        public async Task<BaseResult> Add([FromBody] AddArtistViewModel artist)
        {
            var create = await _artistApplication.Add(artist);
            if (!create.Success)
                return create;

            await _fileUpload.CreateArtist(artist.FullName);

            return create;
        }

        [HttpPut("UpdateArtist")]
        public async Task<BaseResult> Update([FromBody] EditArtistViewModel artist)
        {
            return await _artistApplication.Update(artist);
        }

        [HttpGet("GetDetail")]
        public async Task<ApiResponse<EditArtistViewModel>> GetDetail(string id)
        {
            return (ApiResponse<EditArtistViewModel>)await _artistApplication.GetDetail(new SearchArtistViewModel()
            {
                Id = id
            });
        }

        [HttpGet("Artists")]
        public async Task<ApiResponse<List<ArtistViewModel>>> Artists([FromQuery] SearchArtistViewModel search, [FromQuery] PaginationParameters parameters)
        {
            return (ApiResponse<List<ArtistViewModel>>)await _artistApplication.ToViews(search, parameters, cancellationToken);
        }
    }
}

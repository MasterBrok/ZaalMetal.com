using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZaalMetal.API.Services;

namespace ZaalMetal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {

        private readonly IFileUpload _fileUpload;

        public FileController(IFileUpload fileUpload)
        {
            _fileUpload = fileUpload;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create()
        {
            string bandName = "Epica";
            var pt = await _fileUpload.CreateBand(bandName);

            Console.WriteLine(pt.Path);
            Console.WriteLine(_fileUpload.CreateSubAlbum(bandName, "Omega").Result.Name);
            Console.WriteLine(_fileUpload.CreateSubAlbum(bandName, "Omega Alive").Result.Name);

            return Ok();
        }

    }
}

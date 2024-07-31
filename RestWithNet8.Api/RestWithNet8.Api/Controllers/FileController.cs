using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestWithNet8.Api.Business;
using RestWithNet8.Api.Data.VO;

namespace RestWithNet8.Api.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize("Bearer")]
    public class FileController: ControllerBase
    {
        private readonly IFileBusiness _fileBusiness;

        public FileController(IFileBusiness fileBusiness)
        {
            _fileBusiness = fileBusiness;
        }

        [HttpPost("downloadFile/{fileName}")]
        [ProducesResponseType((200), Type = typeof(byte[]))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("Application/octet-stream")]

        public async Task<IActionResult> GetFileAsync(string fileName)
        {
            byte[] buffer = _fileBusiness.GetFile(fileName);
            if(buffer != null)
            {
                HttpContext.Response.ContentType = $"application/{Path.GetExtension(fileName).Replace(".", "")}";
                HttpContext.Response.Headers.Add("Content-length", buffer.Length.ToString());
                await HttpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length);
            }
            return new ContentResult();
        }

        [HttpPost("uploadFile")]
        [ProducesResponseType((200), Type = typeof(FileDetailVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("Application/json")]

        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            var detail = await _fileBusiness.SaveFileToDisk(file);
            return new OkObjectResult(detail);
        }

        [HttpPost("uploadMultipleFile")]
        [ProducesResponseType((200), Type = typeof(List<FileDetailVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("Application/json")]

        public async Task<IActionResult> UploadManyFiles([FromForm] List<IFormFile> files)
        {
            List<FileDetailVO> details = await _fileBusiness.SaveFilesToDisk(files);
            return new OkObjectResult(details);
        }

        //[HttpPost("upload")]
        //public IActionResult UploadFile([FromForm] IFormFile file)
        //{
        //    FileDetailVO detail =  _fileBusiness.SaveFileToDisk(file);

        //    return new OkObjectResult(detail);
        //}
    }
}

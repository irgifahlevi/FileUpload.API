using FileUpload.API.Filters;
using FileUpload.API.Models;
using FileUpload.API.Models.DataTransferObject;
using FileUpload.API.Repository.Presistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FileUpload.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileHandlingController : ControllerBase
    {
        private readonly IFileRepository _fileRepository;
        private Response response = new Response();

        public FileHandlingController(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        [HttpGet(nameof(GetAll))]
        public async Task<IActionResult> GetAll()
        {
            var data = await _fileRepository.AllFiles();
            response.Result = data;
            response.StatusCode = (int)HttpStatusCode.OK;
            return Ok(response);
        }

        [HttpPost(nameof(Create))]
        public async Task<IActionResult> Create([FromBody] FileRequest fileRequest)
        {

            FileHandling d = new FileHandling()
            {
                ImageCode = fileRequest.ImageCode,
                ImageName = fileRequest.ImageName
            };
            
            await _fileRepository.Add(d);
            response.Result = "Data success created";
            response.StatusCode = (int)HttpStatusCode.Created;
            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] FileRequest fileRequest)
        {
            var fileData = await _fileRepository.GetByCode(fileRequest.ImageCode);

            fileData.ImageName = fileRequest.ImageName;

            await _fileRepository.Update(fileData);
            response.Result = "Data success updated";
            response.StatusCode = (int)HttpStatusCode.Accepted;
            return Ok(response);
        }

        [HttpDelete("DeleteFile/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var fileData = await _fileRepository.GetByID(id);
            if(fileData == null)
            {
                throw new NullReferenceException();
            }
            await _fileRepository.Delete(fileData);
            response.Result = "Data success deleted";
            response.StatusCode = (int)HttpStatusCode.Accepted;
            return Ok(response);
        }

    }
}

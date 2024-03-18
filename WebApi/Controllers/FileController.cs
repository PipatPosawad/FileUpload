using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using WebApi.Dtos;
using WebApi.Infrastructure;

namespace WebApi.Controllers
{
    /// <summary>
    /// Controller for file uploading
    /// </summary>
    [Authorize(Policy = "AuthZPolicy")]
    [ApiController]
    [V1RoutePrefix(FileRoutes.RoutePrefix)]
    [Produces(MediaTypeNames.Application.Json)]
    public class FileController : ControllerBase
    {
        private readonly IFileUploadService _fileUploadService;

        /// <summary>
        /// Initializes a <see cref="FileController"/>
        /// </summary>
        /// <param name="fileUploadService"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public FileController(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService ?? throw new ArgumentNullException(nameof(fileUploadService));
        }

        /// <summary>
        /// Uploads a file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostUploadAsync([Required] IFormFile file)
        {
            if (file.Length > 0)
            {
                var fileStream = file.OpenReadStream();
                var fileModel = await _fileUploadService.UploadAsync(file.FileName, file.ContentType, fileStream);

                // Process uploaded files
                // Don't rely on or trust the FileName property without validation.
                var result = FileDtoMapper.Map(fileModel);
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        //// GET: api/<FileController>
        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<FileController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<FileController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<FileController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<FileController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}

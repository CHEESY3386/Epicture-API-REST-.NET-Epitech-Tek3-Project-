using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Refit;
using Newtonsoft.Json;
using Epicture.API.Services;
using Epicture.API.Models;

namespace Epicture.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        #region MEMBERS

        private readonly ILogger<ImageController> _logger;
        private readonly IImageService _imageService;

        #endregion MEMBERS

        #region CONSTRUCTOR

        public ImageController(ILogger<ImageController> logger)
        {
            _imageService = RestService.For<IImageService>("https://" + "api.imgur.com");
            _logger = logger;
        }

        #endregion CONSTRUCTOR

        #region ROUTES

        [HttpGet("{sort}/{window}/{page}/search")]
        public async Task<IActionResult> GetSearch(
            [FromRoute] string sort,
            [FromRoute] string window,
            [FromRoute] string page,
            [FromQuery] string q,
            [FromHeader] string Authorization
            )
        {
            string response;
            ImgurResponseModel<List<GalleryModel>> model;

            _logger.LogInformation($"Requesting Image search");
            try
            {
                response = await _imageService.GetSearchImages(sort, window, page, q, Authorization);
                model = JsonConvert.DeserializeObject<ImgurResponseModel<List<GalleryModel>>>(response);
            }
            catch (ApiException e)
            {
                _logger.LogError($"Something went wrong with the external API. Details: {e}");
                return StatusCode((int)e.StatusCode);
            }
            return Ok(model);
        }


        [HttpPost("upload")]
        public async Task<IActionResult> PostUpload(
            [FromForm] IFormFile image,
            [FromHeader] string Authorization)
        {
            string upload;
            var streamPart = new StreamPart(image.OpenReadStream(), image.Name, image.ContentType);

            _logger.LogInformation($"Posting image {image}");
            try
            {
                upload = await _imageService.PostImage(streamPart, Authorization);
            }
            catch (ApiException e)
            {
                _logger.LogError($"Something went wrong with the external API. Details: {e}");
                return StatusCode((int)e.StatusCode);
            }
            return Ok(upload);
        }

        #endregion ROUTES
    }
}

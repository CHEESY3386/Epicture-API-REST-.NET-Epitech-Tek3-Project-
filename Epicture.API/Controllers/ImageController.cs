using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Refit;
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

        #region GETS

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

        #endregion GETS

        #region POSTS

        [HttpPost("upload")]
        public async Task<IActionResult> PostUpload(
            [FromHeader] string Authorization,
            [FromForm] IFormFile image)
        {
            string response;
            StreamPart streamPart = new StreamPart(image.OpenReadStream(), image.Name, image.ContentType);
            ImgurResponseModel<ImageModel> model;

            _logger.LogInformation($"Posting image");
            try
            {
                response = await _imageService.PostImage(streamPart, Authorization);
                model = JsonConvert.DeserializeObject<ImgurResponseModel<ImageModel>>(response);
            }
            catch (ApiException e)
            {
                _logger.LogError($"Something went wrong with the external API. Details: {e}");
                return StatusCode((int)e.StatusCode);
            }
            return Ok(model);
        }

        #endregion POSTS

        #region DELETES

        [HttpDelete("{imageHash}/delete")]
        public async Task<IActionResult> DelImage(
            [FromRoute] string imageHash,
            [FromHeader] string Authorization
            )
        {
            string response;
            ImgurResponseModel<bool> model;

            _logger.LogInformation($"Requesting Image search");
            try
            {
                response = await _imageService.DeleteImage(imageHash, Authorization);
                model = JsonConvert.DeserializeObject<ImgurResponseModel<bool>>(response);
            }
            catch (ApiException e)
            {
                _logger.LogError($"Something went wrong with the external API. Details: {e}");
                return StatusCode((int)e.StatusCode);
            }
            return Ok(model);
        }

        #endregion DELETES

        #endregion ROUTES
    }
}

using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Refit;
using Epicture.API.Services;
using Epicture.API.Models;

namespace Epicture.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        #region MEMBERS

        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;

        #endregion MEMBERS

        #region CONSTRUCTOR

        public AccountController(ILogger<AccountController> logger)
        {
            _accountService = RestService.For<IAccountService>("https://" + "api.imgur.com");
            _logger = logger;
        }

        #endregion CONSTRUCTOR

        #region ROUTES

        [HttpGet("{username}")]
        public async Task<IActionResult> GetBase(
            [FromRoute] string username,
            [FromHeader] string Authorization)
        {
            string response;
            ImgurResponseModel<AccountModel> model;

            _logger.LogInformation($"Requesting user base {username}");
            try
            {
                response = await _accountService.GetAccountBase(username, Authorization);
                model = JsonConvert.DeserializeObject<ImgurResponseModel<AccountModel>>(response);
            }
            catch (ApiException e)
            {
                _logger.LogError($"Something went wrong with the external API. Details: {e}");
                return StatusCode((int)e.StatusCode);
            }
            return Ok(model);
        }

        [HttpGet("{username}/images")]
        public async Task<IActionResult> GetImages(
            [FromRoute] string username,
            [FromHeader] string Authorization)
        {
            string response;
            ImgurResponseModel<List<ImageModel>> model;

            _logger.LogInformation($"Requesting user images {username}");
            try
            {
                response = await _accountService.GetAccountImages(username, Authorization);
                model = JsonConvert.DeserializeObject<ImgurResponseModel<List<ImageModel>>>(response);
            }
            catch (ApiException e)
            {
                _logger.LogError($"Something went wrong with the external API. Details: {e}");
                return StatusCode((int)e.StatusCode);
            }
            return Ok(model);
        }

        #endregion ROUTES
    }
}

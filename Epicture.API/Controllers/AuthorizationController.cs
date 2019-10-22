using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Epicture.API.Services;
using Epicture.API.Models;
using Newtonsoft.Json;
using Refit;
using Epicture.API.Params;

namespace Epicture.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : Controller
    {
        #region MEMBERS

        private readonly ILogger<AuthorizationController> _logger;
        private readonly IAuthorizationService _authorizationService;

        #endregion MEMBERS

        #region CONSTRUCTOR

        public AuthorizationController(ILogger<AuthorizationController> logger)
        {
            _authorizationService = RestService.For<IAuthorizationService>("https://" + "api.imgur.com");
            _logger = logger;
        }

        #endregion CONSTRUCTOR

        #region ROUTES

        #region GETS

        [HttpGet("login")]
        public async Task<IActionResult> GetLogin(
            [FromQuery] string client_id,
            [FromQuery] string response_type)
        {
            string response;
            AuthQueryParams query = new AuthQueryParams(client_id, response_type);

            _logger.LogInformation($"Requesting Access Page");
            try
            {
                response = await _authorizationService.GetLoginPage(query);
            }
            catch (ApiException e)
            {
                _logger.LogError($"Something went wrong with the external API. Details: {e}");
                return StatusCode((int)e.StatusCode);
            }
            return Ok(response);
        }

        #endregion GETS

        #region POSTS

        [HttpPost("token")]
        public async Task<IActionResult> PostToken(
            [FromForm] string refresh_token,
            [FromForm] string client_id,
            [FromForm] string client_secret,
            [FromForm] string grant_type)
        {
            string response;
            TokensModel model;

            _logger.LogInformation($"Requesting new access token");
            try
            {
                response = await _authorizationService.PostTokenGen(refresh_token, client_id, client_secret, grant_type);
                model = JsonConvert.DeserializeObject<TokensModel>(response);
            }
            catch (ApiException e)
            {
                _logger.LogError($"Something went wrong with the external API. Details: {e}");
                return StatusCode((int)e.StatusCode);
            }
            return Ok(model);
        }

        #endregion POSTS

        #endregion ROUTES
    }
}

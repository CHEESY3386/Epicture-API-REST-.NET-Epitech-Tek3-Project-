using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Epicture.API.Services;
using Refit;

namespace Epicture.API.Controllers
{
    [ApiController]
    [Route("login")]
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

        [HttpGet]
        public async Task<IActionResult> GetLogin(
            [FromQuery] string client_id,
            [FromQuery] string response_type)
        {
            string response;

            _logger.LogInformation($"Requesting Access Page");
            try
            {
                response = await _authorizationService.GetLoginPage(client_id, response_type);
            }
            catch (ApiException e)
            {
                _logger.LogError($"Something went wrong with the external API. Details: {e}");
                return StatusCode((int)e.StatusCode);
            }
            return Ok(response);
        }

        #endregion ROUTES
    }
}

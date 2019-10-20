using System.Threading.Tasks;
using Refit;

namespace Epicture.API.Services
{
    public interface IAuthorizationService
    {
        [Get("/oauth2/authorize?client_id={clientId}&response_type={responseType}")]
        Task<string> GetLoginPage(
            string clientId,
            string responseType);
    }
}
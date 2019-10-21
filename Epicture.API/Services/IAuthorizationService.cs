using System.Threading.Tasks;
using Refit;

namespace Epicture.API.Services
{
    public interface IAuthorizationService
    {
        [Get("/oauth2/authorize?client_id={clientId}&response_type={responseType}")]
        Task<string> GetLoginPage(
            [AliasAs("clientId")] string clientId,
            [AliasAs("responseType")] string responseType);

        [Multipart]
        [Post("/oauth2/token")]
        Task<string> PostTokenGen(
            [AliasAs("refresh_token")] string refresh_token,
            [AliasAs("client_id")] string clien_id,
            [AliasAs("client_secret")] string client_secret,
            [AliasAs("grant_type")] string grant_type);
    }
}
using System.Threading.Tasks;
using Refit;
using Epicture.API.Params;

namespace Epicture.API.Services
{
    public interface IAuthorizationService
    {
        [Get("/oauth2/authorize")]
        Task<string> GetLoginPage(
            [Query] AuthQueryParams par);

        [Multipart]
        [Post("/oauth2/token")]
        Task<string> PostTokenGen(
            [AliasAs("refresh_token")] string refresh_token,
            [AliasAs("client_id")] string clien_id,
            [AliasAs("client_secret")] string client_secret,
            [AliasAs("grant_type")] string grant_type);
    }
}
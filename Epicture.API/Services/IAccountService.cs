using System.Threading.Tasks;
using Refit;

namespace Epicture.API.Services
{
    public interface IAccountService
    {
        [Get("/3/account/{username}")]
        Task<string> GetAccountBase(
            [AliasAs("username")] string username,
            [Header("Authorization")] string Authorization);

        [Get("/3/account/{username}/images")]
        Task<string> GetAccountImages(
            [AliasAs("username")] string username,
            [Header("Authorization")] string accessToken);
    }
}
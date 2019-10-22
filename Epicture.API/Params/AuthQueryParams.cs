using Refit;

namespace Epicture.API.Params
{
    public class AuthQueryParams
    {
        #region MEMBERS

        [AliasAs("client_id")]
        public string ClientId { get; set; }

        [AliasAs("response_type")]
        public string ResponseType { get; set; }

        #endregion MEMBERS

        #region CONSTRUCTOR

        public AuthQueryParams(string client_id, string response_type)
        {
            ClientId = client_id;
            ResponseType = response_type;
        }

        #endregion CONSTRUCTOR
    }
}

using Newtonsoft.Json;

namespace Epicture.API.Models
{
    public class AccountModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("reputation")]
        public float Reputation { get; set; }

        [JsonProperty("reputation_name")]
        public string ReputationName { get; set; }

        [JsonProperty("created")]
        public int Created { get; set; }

        [JsonProperty("pro_expiration")]
        public bool ProExpiration { get; set; }
    }
}

using Newtonsoft.Json;

namespace Epicture.API.Models
{
    public class ImgurResponseModel<T>
    {
        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }
    }
}

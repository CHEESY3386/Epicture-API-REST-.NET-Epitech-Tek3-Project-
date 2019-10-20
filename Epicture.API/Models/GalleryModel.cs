using Newtonsoft.Json;
using System.Collections.Generic;

namespace Epicture.API.Models
{
    public class GalleryModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("images")]
        public List<ImageModel> Images { get; set; }

    }
}

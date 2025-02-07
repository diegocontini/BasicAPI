using System.Text.Json.Serialization;

namespace BasicAPI.Models.Models
{
    public class PaginatedResult<T>
    {

        [JsonPropertyName("page")]
        public int Page { get; set; }
        [JsonPropertyName("pages")]
        public int Pages { get; set; }
        [JsonPropertyName("total")]
        public int Total { get; set; }
        [JsonPropertyName("data")]
        public IEnumerable<T> Data { get; set; }

    }
}

using System.Text.Json.Serialization;

namespace BasicAPI.Features.Shared
{
    public class PaginatedResult<T>
    {

        [JsonPropertyNameAttribute("page")]
        public int Page { get; set; }
        [JsonPropertyNameAttribute("pages")]
        public int Pages { get; set; }
        [JsonPropertyNameAttribute("total")]
        public int Total { get; set; }
        [JsonPropertyNameAttribute("data")]
        public IEnumerable<T> Data { get; set; }

    }
}

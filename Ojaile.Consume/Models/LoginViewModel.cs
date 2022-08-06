using Newtonsoft.Json;

namespace Petify.Consume.Models
{
    public class LoginViewModel
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }

    public class LoginTokenise
    {
        [JsonProperty]
        public string token { get; set; }

        [JsonProperty]
        public string? lastname { get; set; }

        [JsonProperty]
        public string? firstname { get; set; }

        [JsonProperty]
        public string? email { get; set; }
    }
}

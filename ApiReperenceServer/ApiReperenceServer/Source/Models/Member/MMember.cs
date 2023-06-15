using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System.ComponentModel;

namespace ApiReperenceServer.Source.Models.Posts
{
    public class MMember
    {
        [JsonProperty("memberNo")]
        public int NO { get; set; }
        [JsonProperty("id")]
        public string? ID { get; set; }
        [JsonProperty("password")]
        public string? PASSWORD { get; set; }
        [JsonProperty("token")]
        public string? TOKEN { get; set; }
        [JsonProperty("createdAt")]
        public DateTime CREATED_AT { get; set; }

    }
}

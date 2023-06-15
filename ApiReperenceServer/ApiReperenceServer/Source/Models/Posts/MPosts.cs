using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System.ComponentModel;

namespace ApiReperenceServer.Source.Models.Posts
{
    /// <summary>
    /// 포스트 모델
    /// </summary>
    public class MPosts
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("title")]
        public string? TITLE { get; set; }

        [JsonProperty("content")]
        public string? CONTENT { get; set; }

        [JsonProperty("createAt")]
        public DateTime CREATE_AT { get; set; }

        [JsonProperty("author")]
        public string? AUTHOR { get; set; }


    }
}

using ApiReperenceServer.Source.Models.Posts;
using Dapper;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiReperenceServer.Source.Models.Posts
{
    public class MGetPostList
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("title")]
        public string? TITLE { get; set; }

        [JsonProperty("createAt")]
        public string? CREATE_AT { get; set; }

        [JsonProperty("author")]
        public string? AUTHOR { get; set; }

    }
}

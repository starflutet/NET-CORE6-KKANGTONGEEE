using ApiReperenceServer.Source.Models.Posts;
using Dapper;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiReperenceServer.Source.Models.Posts
{
    /// <summary>
    /// 주의사항!!
    /// 반드시 디비의 컬럼명과 변수명을 동일하게 해야함! 대소문자 까지!!
    /// 대신 API 응답값으로 줄땐 JsonProperty 여기의 값으로 커스텀해서 줄수있음!!
    /// </summary>
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

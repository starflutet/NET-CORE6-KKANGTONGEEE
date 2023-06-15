using ApiReperenceServer.Source.Models.Posts;
using ApiReperenceServer.Source.Serialize.Default;
using Newtonsoft.Json;

namespace ApiReperenceServer.Source.Serialize.Member
{
    public class ResLogin : ResDefault
    {
        /// <summary>
        /// 포스트 결과 데이터
        /// </summary>
        [JsonProperty("data")]
        public List<MMember>? Data { get; set; }

    }
}

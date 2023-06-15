using ApiReperenceServer.Source.Models.Posts;
using ApiReperenceServer.Source.Serialize.Default;
using Newtonsoft.Json;

namespace ApiReperenceServer.Source.Serialize.Posts
{
    /// <summary>
    /// 포스트목록
    /// </summary>
    public class ResGetPostList : ResDefault
    {
        /// <summary>
        /// 포스트 결과 데이터
        /// </summary>
        [JsonProperty("data")]
        public List<MGetPostList>? Data { get; set; }

    }
}

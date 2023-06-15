using ApiReperenceServer.Source.Models.Posts;
using Newtonsoft.Json;

namespace ApiReperenceServer.Source.Serialize.Member
{
    public class ResLogin
    {
        /// <summary>
        /// 결과코드
        /// </summary>
        [JsonProperty("resultCode")]
        public string? Result_Code { get; set; }

        /// <summary>
        /// 결과 메시지
        /// </summary>
        [JsonProperty("resultMsg")]
        public string? Result_Msg { get; set; }

        /// <summary>
        /// 포스트 결과 데이터
        /// </summary>
        [JsonProperty("data")]
        public List<MMember>? Data { get; set; }


    }
}

using ApiReperenceServer.Source.Models.Posts;
using Newtonsoft.Json;

namespace ApiReperenceServer.Source.Serialize.Default
{
    public class ResDefault
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

    }
}

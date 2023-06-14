using ApiReperenceServer.Source.Models.Posts;
using Newtonsoft.Json;

namespace ApiReperenceServer.Source.Serialize.Posts
{
    public class ResGetPostList
    {
        #region [결과코드]
        [JsonProperty("resultCode")]
        public string? Result_Code { get; set; }
        #endregion

        #region [결과메시지]
        [JsonProperty("resultMsg")]
        public string? Result_Msg { get; set; }
        #endregion               

        #region [포스트 데이터]
        [JsonProperty("data")]
        public List<MGetPostList>? Data { get; set; }
        #endregion        

    }
}

using ApiReperenceServer.Source.Models.Posts;

namespace ApiReperenceServer.Source.Serialize.Posts
{
    public class ResponseGetPostList
    {
        #region [결과코드]
        public string? Result_Code { get; set; }
        #endregion

        #region [결과메시지]
        public string? Result_Msg { get; set; }
        #endregion               

        #region [포스트 데이터]
        public List<MGetPostList>? Data { get; set; }
        #endregion        

    }
}

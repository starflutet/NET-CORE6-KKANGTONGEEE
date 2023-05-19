using ApiReperenceServer.Source.Models;
namespace ApiReperenceServer.Source.Serialize
{
    public class responsePOSTS
    {
        #region [결과코드]
        public string? Result_Code { get; set; }
        #endregion

        #region [결과메시지]
        public string? Result_Msg { get; set; }
        #endregion               

        #region [포스트 데이터]
        public List<Posts> Data { get; set; }
        #endregion        

    }
}

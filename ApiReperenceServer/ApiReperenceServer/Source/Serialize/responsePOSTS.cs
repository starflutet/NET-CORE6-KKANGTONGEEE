using ApiReperenceServer.Source.Models;
namespace ApiReperenceServer.Source.Serialize
{
    public class responsePOSTS
    {
        #region [����ڵ�]
        public string? Result_Code { get; set; }
        #endregion

        #region [����޽���]
        public string? Result_Msg { get; set; }
        #endregion               

        #region [����Ʈ ������]
        public List<Posts> Data { get; set; }
        #endregion        

    }
}
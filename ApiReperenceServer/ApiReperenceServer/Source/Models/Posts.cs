using Oracle.ManagedDataAccess.Client;

namespace ApiReperenceServer.Source.Models
{
    public class Posts
    {
        #region [���̵�]
        public int Id { get; set; }
        #endregion

        #region [����]
        public string Title { get; set; }
        #endregion

        #region [����]
        public string Content { get; set; }
        #endregion

        #region [�ۼ��Ͻ�]
        public DateTime CreatedAt { get; set; }
        #endregion

        #region [�ۼ��� ����]
        public string Author { get; set; }
        #endregion


    }
}
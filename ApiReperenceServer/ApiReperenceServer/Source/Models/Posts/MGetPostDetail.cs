using ApiReperenceServer.Source.Models.Posts;

namespace ApiReperenceServer.Source.Models.Posts
{
    public class MGetPostDetail
    {
        #region [아이디]
        public int Id { get; set; }
        #endregion

        #region [제목]
        public string? Title { get; set; }
        #endregion

        #region [내용]
        public string? Content { get; set; }
        #endregion

        #region [작성일시]
        public string CreatedAt { get; set; }
        #endregion

    }
}

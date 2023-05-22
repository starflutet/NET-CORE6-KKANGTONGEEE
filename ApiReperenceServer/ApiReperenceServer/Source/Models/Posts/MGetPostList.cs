using ApiReperenceServer.Source.Models.Posts;

namespace ApiReperenceServer.Source.Models.Posts
{
    public class MGetPostList
    {
        #region [아이디]
        public int Id { get; set; }
        #endregion

        #region [제목]
        public string? Title { get; set; }
        #endregion   

        #region [작성일시]
        public DateTime CreatedAt { get; set; }
        #endregion

        #region [작성자 제목]
        public string? Author { get; set; }
        #endregion
    }
}

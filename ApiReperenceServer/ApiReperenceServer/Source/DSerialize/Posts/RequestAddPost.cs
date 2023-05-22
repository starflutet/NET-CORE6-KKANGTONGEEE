namespace ApiReperenceServer.Source.DSerialize
{
    public class RequestAddPost
    {
        #region [제목]
        public string? Title { get; set; }
        #endregion

        #region [내용]
        public string? Content { get; set; }
        #endregion

        #region [작성일시]
        public string? CreatedAt { get; set; }
        #endregion

        #region [작성자 제목]
        public string? Author { get; set; }
        #endregion
    }
}

namespace ApiReperenceServer.Source.DSerialize
{
    /// <summary>
    /// 공용으로 사용할 Req 값입니다.
    /// </summary>
    public class ReqPost
    {
        /// <summary>
        /// 제목
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 내용
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// 생성일자
        /// </summary>
        public string? CreatedAt { get; set; }

        /// <summary>
        /// 작성자
        /// </summary>
        public string? Author { get; set; }
    }
}

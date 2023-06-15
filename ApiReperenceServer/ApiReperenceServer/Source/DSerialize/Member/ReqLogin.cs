namespace ApiReperenceServer.Source.DSerialize.Member
{
    /// <summary>
    /// 로그인 요청 파라미터
    /// </summary>
    public class ReqLogin
    {
        /// <summary>
        /// 사용자 아이디
        /// </summary>
        public string? MemberId { get; set; }

        /// <summary>
        /// 사용자 비밀번호
        /// </summary>
        public string? MemberPw { get; set; }

    }

}

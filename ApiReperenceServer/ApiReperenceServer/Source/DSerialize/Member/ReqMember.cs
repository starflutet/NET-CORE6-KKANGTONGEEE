namespace ApiReperenceServer.Source.DSerialize.Member
{
    /// <summary>
    /// 공용으로 사용할 Req 값입니다.
    /// </summary>
    public class ReqMember
    {
        /// <summary>
        /// 사용자 아이디
        /// </summary>
        public string? MemberId { get; set; }

        /// <summary>
        /// 사용자 비밀번호
        /// </summary>
        public string? MemberPw { get; set; }

        /// <summary>
        /// 사용자 식별자
        /// </summary>
        public int? MemberNo { get; set; }

        /// <summary>
        /// 토큰값
        /// </summary>
        public string? MemberToken { get; set; }

    }

}

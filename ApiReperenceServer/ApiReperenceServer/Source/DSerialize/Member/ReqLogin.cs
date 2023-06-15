namespace ApiReperenceServer.Source.DSerialize.Member
{
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

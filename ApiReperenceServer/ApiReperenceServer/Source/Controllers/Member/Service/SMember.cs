
using ApiReperenceServer.Source.App;
using ApiReperenceServer.Source.Controllers.Member.Adaptor;
using ApiReperenceServer.Source.Controllers.Posts.Adaptor;
using ApiReperenceServer.Source.DSerialize;
using ApiReperenceServer.Source.DSerialize.Member;
using ApiReperenceServer.Source.Serialize.Default;
using ApiReperenceServer.Source.Serialize.Member;
using ApiReperenceServer.Source.Serialize.Posts;

namespace ApiReperenceServer.Source.Controllers.Member.Service
{
    public class SMember
    {
        /// <summary>
        /// 기존유저있는지 체크
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static ResLogin Login(ReqLogin request)
        {
            ResLogin response = AMember.Login(request);

            return response;
        }

        /// <summary>
        /// 토큰발급 후, 세팅
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string SetToken(int memberNo)
        {
            string token = MyUtils.GenerateToken();

            ResDefault response = AMember.SetToken(token, memberNo);

            if (response.Result_Code!.Equals("0"))
            {
                return token;
            }
            else
            {
                return "";
            }
        }
    }
}

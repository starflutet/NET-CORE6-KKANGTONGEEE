using ApiReperenceServer.Source.Controllers.Posts.Adaptor;
using ApiReperenceServer.Source.DSerialize;
using ApiReperenceServer.Source.Serialize.Posts;

namespace ApiReperenceServer.Source.Controllers.Posts.Service
{
    /// <summary>
    /// 서비스 클래스 디비동작을 제외한 로직을 처리합니다.
    /// </summary>
    public class SPosts
    {
        public static ResGetPostList GetPostList(ReqGetPostList request)
        {
            ResGetPostList response = APosts.GetPostList(request);

            return response;
        }
    }
}

using ApiReperenceServer.Source.Controllers.Posts.Adaptor;
using ApiReperenceServer.Source.DSerialize;
using ApiReperenceServer.Source.Serialize.Posts;

namespace ApiReperenceServer.Source.Controllers.Posts.Service
{
    public class SPosts
    {
        #region [구현 - 포스트목록조회]
        public static ResGetPostList GetPostList(ReqGetPostList request)
        {
            ResGetPostList response = APosts.GetPostList(request);

            return response;
        }
        #endregion        

        #region [구현 - 포스트목록조회 2]
        public static Dictionary<string, object> GetPostListTwo(Dictionary<string, object> request)
        {
            Dictionary<string, object> response = APosts.GetPostListTwo(request);

            return response;
        }
        #endregion

        #region [구현 - 포스트목록조회 3]
        public static Dictionary<string, object> GetPostListThree(ReqGetPostList request)
        {
            Dictionary<string, object> response = APosts.GetPostListThree(request);

            return response;
        }
        #endregion

        #region [구현 - 포스트목록조회4]
        public static ResGetPostList GetPostListFore(ReqGetPostList request)
        {
            ResGetPostList response = APosts.GetPostListFore(request);

            return response;
        }
        #endregion

        #region [구현 - 포스트목록조회5]
        public static ResGetPostList GetPostListFive(ReqGetPostList request)
        {
            ResGetPostList response = APosts.GetPostListFive(request);

            return response;
        }
        #endregion

        #region [구현 - 포스트목록상세]
        public static ResGetPostDetail GetPostDetail(ReqGetPostDetail request)
        {
            ResGetPostDetail response = APosts.GetPostDetail(request);

            return response;
        }
        #endregion

        #region [구현 - 포스트 등록]
        public static ResAction AddPost(ReqPost request)
        {
            ResAction response = APosts.AddPost(request);

            return response;
        }
        #endregion
    }
}

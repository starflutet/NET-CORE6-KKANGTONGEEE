using ApiReperenceServer.Source.Controllers.Posts.Service;
using ApiReperenceServer.Source.DSerialize;
using ApiReperenceServer.Source.Models.Posts;
using ApiReperenceServer.Source.Serialize.Posts;
using Microsoft.AspNetCore.Mvc;

namespace ApiReperenceServer.Source.Controllers.Posts
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PostControllers : ControllerBase
    {

        private readonly ILogger<PostControllers> _logger;

        public PostControllers(ILogger<PostControllers> logger)
        {
            _logger = logger;
        }

        #region [목록조회 시리즈들 - 난 4번으로 할것임]
        #region [포스트 목록 조회]
        //파라미터 객체 응답값 객체
        //장점 ::: 받을값과 응답값을 내가 커스텀할수있음 + 스웨거 문서가 맛있게 만들어짐
        //단점 ::: 개발 속도만 보면 다른 2개보다 속도가 느림 /// 이유는 직접 객체들을 관리해야해서
        /// <summary>
        /// 포스트 목록 조회
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(Name = "GetPostList")]
        public IEnumerable<ResponseGetPostList> GetPostList(RequestGetPostList request)
        {
            ResponseGetPostList response = PostsImpl.GetPostList(request);

            yield return response;
        }
        #endregion

        #region [포스트 목록 조회 2] - 파라미터를 딕셔너리으로 받아줌
        //파라미터 딕셔너리(맵) 응답값 딕셔너리 (맵)
        //장점 ::: 개발속도만 보면 이게 제일 빠를거임 /// 이유는 값세팅일 알아서 다되니까 그럼
        //단점 ::: 유지보수가 힘들어질수있고, 파라미터 예시를 커스텀할수없음 => NET CORE 는 다를줄알았는데.. 스프링때랑 똑같이 맵 객체는 파라미터 커스텀 못함 ㅠ 
        /// <summary>
        /// 포스트 목록 조회2
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(Name = "GetPostListTwo")]
        public IEnumerable<Dictionary<string, object>> GetPostListTwo(Dictionary<string, object> request)
        {
            Dictionary<string, object> response = PostsImpl.GetPostListTwo(request);

            yield return response;
        }
        #endregion

        #region [포스트 목록 조회 3] - 파라미터를 세팅한 객체로 받아줌
        // 파라미터 객체 응답값 딕셔너리 (맵)
        // 장점 ::: 파라미터를 관리할수있기에, 2번에서 못한 Example Value 값 설정가능, 객체로 받기때문에 SwaggerFillter를 통해 세부적 커스텀도 가능함
        // 단점 ::: 응답값을 Dictionary로 주기 때문에, 디비쿼리 잘짜야함 = 쿼리에서 내려주는 결과에따라 응답값이 변하기때문
        /// <summary>
        /// 포스트 목록 조회 3
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(Name = "GetPostListThree")]
        public IEnumerable<Dictionary<string, object>> GetPostListThree(RequestGetPostList request)
        {
            Dictionary<string, object> response = PostsImpl.GetPostListThree(request);

            yield return response;
        }
        #endregion

        #region [포스트 목록 조회4]
        //파라미터 객체 응답값 객체
        //1번 방식을 좀더 커스텀함, 모델도 커스텀하고싶어서 4번이 생겼다 봐도 무방함
        //장점 ::: 받을값과 응답값을 내가 커스텀할수있음 + 스웨거 문서가 맛있게 만들어짐
        //단점 ::: 개발 속도만 보면 다른 방식보다 속도가 느림 /// 이유는 직접 객체들을 관리해야해서
        /// <summary>
        /// 포스트 목록 조회
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(Name = "GetPostListFore")]
        public IEnumerable<ResponseGetPostList> GetPostListFore(RequestGetPostList request)
        {
            ResponseGetPostList response = PostsImpl.GetPostListFore(request);

            yield return response;
        }
        #endregion
        #endregion

        #region [포스트 목록 상세]        
        [HttpPost(Name = "GetPostDetail")]
        public IEnumerable<ResponseGetPostDetail> GetPostDetail(RequestGetPostDetail request)
        {
            ResponseGetPostDetail response = PostsImpl.GetPostDetail(request);

            yield return response;
        }
        #endregion

        #region [포스트 등록]        
        [HttpPost(Name = "AddPost")]
        public IEnumerable<ResponseAction> AddPost(RequestAddPost request)
        {
            ResponseAction response = PostsImpl.AddPost(request);

            yield return response;
        }
        #endregion
    }
}

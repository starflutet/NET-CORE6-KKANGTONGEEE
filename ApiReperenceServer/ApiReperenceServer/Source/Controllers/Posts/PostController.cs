using ApiReperenceServer.Source.Controllers.Posts.Service;
using ApiReperenceServer.Source.DSerialize;
using ApiReperenceServer.Source.Serialize.Posts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        /// <summary>
        /// 포스트 목록 조회
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(Name = "GetPostList")]
        public IActionResult GetPostList(ReqGetPostList request)
        {
            ResGetPostList response = SPosts.GetPostList(request);

            string json = JsonConvert.SerializeObject(response, Formatting.None);

            return Content(json, "application/json");
        }
    }
}

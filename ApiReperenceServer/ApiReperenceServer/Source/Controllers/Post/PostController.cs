using ApiReperenceServer.Source.Controllers.Post.Service;
using ApiReperenceServer.Source.DSerialize;
using ApiReperenceServer.Source.Models;
using ApiReperenceServer.Source.Serialize;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace ApiReperenceServer.Source.Controllers.Post
{
    [ApiController]
    [Route("api/post/[controller]")]
    public class PostControllers : ControllerBase
    {

        private readonly ILogger<PostControllers> _logger;

        public PostControllers(ILogger<PostControllers> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "GetPostList")]
        public IEnumerable<responsePOSTS> Get(requestPOSTS request)
        {
            responsePOSTS response = PostsImpl.GetPostList(request);

            yield return response;
        }
    }
}

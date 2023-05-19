using ApiReperenceServer.Source.Controllers.Posts.Service;
using ApiReperenceServer.Source.DSerialize;
using ApiReperenceServer.Source.Models;
using ApiReperenceServer.Source.Serialize;
using ApiReperenceServer.Source.Serialize.Posts;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;

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

        #region [포스트 목록 조회]        
        [HttpPost(Name = "GetPostList")]
        public IEnumerable<ResponseGetPostList> GetPostList(RequestGetPostList request)
        {

            ResponseGetPostList response = PostsImpl.GetPostList(request);

            yield return response;
        }
        #endregion

        #region [포스트 목록 조회 2]
        [HttpPost(Name = "GetPostListTwo")]
        public IEnumerable<Dictionary<string, object>> GetPostListTwo(Dictionary<string, object> request)
        {

            Dictionary<string, object> response = PostsImpl.GetPostListTwo(request);

            yield return response;
        }
        #endregion

        #region [포스트 목록 상세]        
        [HttpPost(Name = "GetPostDetail")]
        public IEnumerable<ResponseGetPostDetail> GetPostDetail(RequestGetPostDetail request)
        {

            ResponseGetPostDetail response = PostsImpl.GetPostDetail(request);

            yield return response;
        }
        #endregion
    }
}

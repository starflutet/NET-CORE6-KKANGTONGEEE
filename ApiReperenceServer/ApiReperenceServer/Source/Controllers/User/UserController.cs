using ApiReperenceServer.Source.Controllers.Posts.Service;
using ApiReperenceServer.Source.DSerialize;
using ApiReperenceServer.Source.Models.Posts;
using ApiReperenceServer.Source.Serialize.Posts;
using Microsoft.AspNetCore.Mvc;

namespace ApiReperenceServer.Source.Controllers.User
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserControllers : ControllerBase
    {

        private readonly ILogger<UserControllers> _logger;

        public UserControllers(ILogger<UserControllers> logger)
        {
            _logger = logger;
        }

        #region [로그인]      
        /// <summary>
        /// 로그인
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(Name = "Login")]
        public void Login()
        {
            HttpContext.Session.SetString("LoginChk", "Y");
        }
        #endregion

        #region [로그아웃]      
        /// <summary>
        /// 로그아웃
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(Name = "Logout")]
        public void Logout()
        {
            HttpContext.Session.Remove("LoginChk");
        }
        #endregion


    }
}

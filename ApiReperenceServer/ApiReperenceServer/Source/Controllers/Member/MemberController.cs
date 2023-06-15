using ApiReperenceServer.Source.Controllers.Member.Service;
using ApiReperenceServer.Source.Controllers.Posts.Service;
using ApiReperenceServer.Source.DSerialize;
using ApiReperenceServer.Source.DSerialize.Member;
using ApiReperenceServer.Source.Models.Posts;
using ApiReperenceServer.Source.Serialize.Default;
using ApiReperenceServer.Source.Serialize.Member;
using ApiReperenceServer.Source.Serialize.Posts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiReperenceServer.Source.Controllers.Member
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MemberControllers : ControllerBase
    {

        private readonly ILogger<MemberControllers> _logger;

        public MemberControllers(ILogger<MemberControllers> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 로그인
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>    

        [HttpPost(Name = "Login")]
        public IActionResult Login(ReqLogin request)
        {
            ResLogin response = SMember.Login(request);

            if (response.Result_Code!.Equals("0"))
            {
                //토큰 업데이트
                string token = SMember.SetToken(response.Data![0].NO);
                response.Data = new List<MMember> { new MMember() };
                response.Data![0].TOKEN = token;
            }

            string json = JsonConvert.SerializeObject(response, Formatting.None);

            return Content(json, "application/json");
        }

    }
}

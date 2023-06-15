using ApiReperenceServer.Source.App;

using ApiReperenceServer.Source.App.DapperUtils;
using ApiReperenceServer.Source.DSerialize;
using ApiReperenceServer.Source.Models.Posts;
using ApiReperenceServer.Source.Serialize.Posts;
using Dapper;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using static ApiReperenceServer.Source.App.Constants;

namespace ApiReperenceServer.Source.Controllers.Posts.Adaptor
{
    /// <summary>
    /// 어뎁터 클래스 - 디비동작과 관련된 로직을 처리합니다.
    /// </summary>
    public class APosts
    {
        /// <summary>
        /// 포스트 목록 조회
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static ResGetPostList GetPostList(ReqGetPostList request)
        {
            ResGetPostList response = new();

            using (OracleConnection connection = new(DataBaseConf.ConnectionStrings))
            {
                OracleDynamicParameters parameters = new();

                parameters.AddIn("in_limit", Convert.ToInt32(request.LimitCnt), OracleDbType.Int32);
                parameters.AddOut("out_result_code", OracleDbType.Varchar2, ParameterDirection.Output, DataLength.out_code);
                parameters.AddOut("out_result_message", OracleDbType.Varchar2, ParameterDirection.Output, DataLength.out_long_msg);
                parameters.AddOutRef("out_result_data", OracleDbType.RefCursor, ParameterDirection.Output);

                var resultList = connection.Query<MGetPostList>
                (
                    "PKG_POSTS.GetPostList",
                    param: parameters,
                    commandType: CommandType.StoredProcedure
                )
                .ToList();

                int out_result_code_index = parameters.oracleParameterList.FindIndex(p => p.ParameterName == "out_result_code");
                int out_result_message_index = parameters.oracleParameterList.FindIndex(p => p.ParameterName == "out_result_message");

                string? out_result_code = Convert.ToString(parameters.oracleParameterList[out_result_code_index].Value);
                string? out_result_message = Convert.ToString(parameters.oracleParameterList[out_result_message_index].Value);

                response.Result_Code = out_result_code;
                response.Result_Msg = out_result_message;
                response.Data = resultList;

            }

            return response;
        }
    }
}

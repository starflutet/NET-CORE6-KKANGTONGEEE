using ApiReperenceServer.Source.App;

using ApiReperenceServer.Source.App.DapperUtils;
using ApiReperenceServer.Source.DSerialize;
using ApiReperenceServer.Source.DSerialize.Member;
using ApiReperenceServer.Source.Models.Posts;
using ApiReperenceServer.Source.Serialize.Default;
using ApiReperenceServer.Source.Serialize.Member;
using ApiReperenceServer.Source.Serialize.Posts;
using Dapper;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using static ApiReperenceServer.Source.App.Constants;

namespace ApiReperenceServer.Source.Controllers.Member.Adaptor
{
    public class AMember
    {
        public static ResLogin Login(ReqLogin request)
        {
            //응답값 초기화
            ResLogin response = new();

            using (OracleConnection connection = new(DataBaseConf.ConnectionStrings))
            {
                OracleDynamicParameters parameters = new();

                parameters.AddIn("in_member_id", Convert.ToString(request.MemberId ?? ""), OracleDbType.Varchar2);
                parameters.AddIn("in_member_pw", Convert.ToString(request.MemberPw ?? ""), OracleDbType.Varchar2);
                parameters.AddOut("out_result_code", OracleDbType.Varchar2, ParameterDirection.Output, DataLength.out_code);
                parameters.AddOut("out_result_message", OracleDbType.Varchar2, ParameterDirection.Output, DataLength.out_long_msg);
                parameters.AddOutRef("out_result_data", OracleDbType.RefCursor, ParameterDirection.Output);

                var resultList = connection.Query<MMember>
                (
                    "PKG_MEMBER.Login",
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

        public static ResDefault SetToken(string token, int memberNo)
        {
            //응답값 초기화
            ResDefault response = new();

            using (OracleConnection connection = new(DataBaseConf.ConnectionStrings))
            {
                OracleDynamicParameters parameters = new();

                parameters.AddIn("in_token_val", Convert.ToString(token), OracleDbType.Varchar2);
                parameters.AddIn("in_member_no", Convert.ToInt32(memberNo), OracleDbType.Int32);
                parameters.AddOut("out_result_code", OracleDbType.Varchar2, ParameterDirection.Output, DataLength.out_code);
                parameters.AddOut("out_result_message", OracleDbType.Varchar2, ParameterDirection.Output, DataLength.out_long_msg);

                var resultList = connection.Query<dynamic>
                (
                    "PKG_MEMBER.SetToken",
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

            }

            return response;
        }

        public static ResDefault Logout(ReqMember request)
        {
            //응답값 초기화
            ResDefault response = new();

            using (OracleConnection connection = new(DataBaseConf.ConnectionStrings))
            {
                OracleDynamicParameters parameters = new();

                parameters.AddIn("in_member_no", request.MemberNo!, OracleDbType.Varchar2);
                parameters.AddOut("out_result_code", OracleDbType.Varchar2, ParameterDirection.Output, DataLength.out_code);
                parameters.AddOut("out_result_message", OracleDbType.Varchar2, ParameterDirection.Output, DataLength.out_long_msg);

                var resultList = connection.Query<MMember>
                (
                    "PKG_MEMBER.Logout",
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

            }

            return response;
        }

    }
}

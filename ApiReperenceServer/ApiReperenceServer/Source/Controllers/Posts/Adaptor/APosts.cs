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
    public class APosts
    {
        #region [목록을 가져오기위한 반복 메소드]
        public static List<MPosts> selectListTwo(OracleCommand command)
        {
            List<MPosts> resultList = new();

            using (OracleDataReader dataReader = command.ExecuteReader())
            {
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        MPosts obj = new MPosts();

                        #region [객체의 프로퍼티 명 가져오기]
                        Type postsType = typeof(MPosts);
                        PropertyInfo[] properties = postsType.GetProperties();
                        #endregion

                        #region [프로머티 명으로 반복실행]
                        foreach (PropertyInfo property in properties)
                        {
                            string propertyName = property.Name.ToLower().Replace("_", "");
                            for (int i = 0; i < dataReader.VisibleFieldCount; i++)
                            {
                                string dataColName = dataReader.GetName(i).ToLower().Replace("_", "");
                                if (propertyName.Equals(dataColName))
                                {
                                    //obj = setPostObj(propertyName, obj, dataReader.GetValue(i));
                                }
                            }
                        }
                        #endregion
                        resultList.Add(obj);
                    }
                }
                return resultList;
            }
        }
        #endregion

        #region [목록을 가져오기위한 반복 메소드 2]
        public static List<Dictionary<string, object>> selectList(OracleCommand command)
        {
            List<Dictionary<string, object>> resultList = new();

            using OracleDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    Dictionary<string, object> obj = new();

                    for (int i = 0; i < dataReader.VisibleFieldCount; i++)
                    {
                        obj.Add(dataReader.GetName(i), dataReader.GetValue(i));
                    }

                    resultList.Add(obj);
                }
            }
            return resultList;
        }
        #endregion       

        #region [상세 조회]
        public static MPosts selectDetail(OracleCommand command)
        {
            MPosts resultDetail = new();

            using (OracleDataReader dataReader = command.ExecuteReader())
            {
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        #region [객체의 프로퍼티 명 가져오기]
                        Type postsType = typeof(MPosts);
                        PropertyInfo[] properties = postsType.GetProperties();
                        #endregion

                        #region [프로머티 명으로 반복실행]
                        foreach (PropertyInfo property in properties)
                        {
                            string propertyName = property.Name.ToLower().Replace("_", "");
                            for (int i = 0; i < dataReader.VisibleFieldCount; i++)
                            {
                                string dataColName = dataReader.GetName(i).ToLower().Replace("_", "");
                                if (propertyName.Equals(dataColName))
                                {
                                    //resultDetail = setPostObj(propertyName, resultDetail, dataReader.GetValue(i));
                                }
                            }
                        }
                        #endregion
                    }
                }
                return resultDetail;
            }
        }
        #endregion

        #region [상세 조회- 두번째 방식]
        public static Dictionary<string, object> selectOne(OracleCommand command)
        {
            Dictionary<string, object> resultDetail = new();

            using OracleDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    for (int i = 0; i < dataReader.VisibleFieldCount; i++)
                    {
                        string dataColName = dataReader.GetName(i).ToLower().Replace("_", "");

                        resultDetail.Add(dataColName, dataReader.GetValue(i));
                    }
                }
            }
            return resultDetail;
        }
        #endregion

        /*#region [Posts-프로퍼티 세팅 메소드]
        public static MPosts setPostObj(string propertiesName, MPosts obj, object value)
        {
            switch (propertiesName)
            {
                case "id":
                    obj.Id = Convert.ToInt32(value);
                    return obj;
                case "title":
                    obj.Title = Convert.ToString(value);
                    return obj;
                case "content":
                    obj.Content = Convert.ToString(value);
                    return obj;
                case "createdat":
                    obj.CreatedAt = Convert.ToDateTime(value);
                    return obj;
                case "author":
                    obj.Author = Convert.ToString(value);
                    return obj;
            }
            return obj;
        }
        #endregion*/

        #region [등록,수정,삭제 용도 = 동작만 하면 되는애들]
        public static Dictionary<string, object> acttionDb(OracleCommand command)
        {
            Dictionary<string, object> resultDetail = new();

            using OracleDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    for (int i = 0; i < dataReader.VisibleFieldCount; i++)
                    {
                        string dataColName = dataReader.GetName(i).ToLower().Replace("_", "");

                        resultDetail.Add(dataColName, dataReader.GetValue(i));
                    }
                }
            }
            return resultDetail;
        }
        #endregion

        #region [구현 - 포스트목록조회]
        public static ResGetPostList GetPostList(ReqGetPostList request)
        {
            ResGetPostList response = new ResGetPostList();

            using (OracleConnection connection = new OracleConnection(DataBaseConf.ConnectionStrings))
            {
                using OracleCommand command = new OracleCommand
                {
                    Connection = connection,
                    CommandText = "PKG_POSTS.GetPostList",
                    CommandType = CommandType.StoredProcedure
                };
                try
                {
                    #region [리스트 초기화]
                    List<MPosts> postsList = new List<MPosts>();
                    #endregion

                    #region [입력 파라미터]
                    command.Parameters.Add(new OracleParameter("in_limit", OracleDbType.Int32)).Value = request.LimitCnt;
                    #endregion

                    #region [리턴 파라미터]
                    command.Parameters.Add(new OracleParameter("out_result_code", OracleDbType.Varchar2, DataLength.out_code)).Direction = ParameterDirection.Output;
                    command.Parameters.Add(new OracleParameter("out_result_message", OracleDbType.Varchar2, DataLength.out_long_msg)).Direction = ParameterDirection.Output;
                    command.Parameters.Add(new OracleParameter("out_result_data", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    #endregion

                    #region [디비 커넥션 연결] 
                    connection.Open();
                    #endregion

                    #region [디비사용]
                    //response.Data = selectList(command);
                    #endregion

                    #region [결과값 세팅]
                    response.Result_Code = command.Parameters["out_result_code"].Value.ToString();
                    response.Result_Msg = command.Parameters["out_result_message"].Value.ToString();
                    #endregion
                }
                catch (Exception ex)
                {
                    #region [에러 메시지 세팅]
                    response.Result_Code = "ERROR";
                    response.Result_Msg = ex.Message;

                    if (LogUtil.ErrLogYn == true)
                    {
                        LogUtil.ErrLogParam("Post-GetPostList", request);
                        LogUtil.ErrLogLine();
                        LogUtil.ErrLogMsg(ex);
                        LogUtil.ErrLogLine();
                    }
                    #endregion
                }
            }
            return response;
        }
        #endregion        

        #region [구현 - 포스트목록조회 2]
        public static Dictionary<string, object> GetPostListTwo(Dictionary<string, object> request)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();

            using (OracleConnection connection = new OracleConnection(DataBaseConf.ConnectionStrings))
            {
                using (
                    OracleCommand command = new OracleCommand
                    {
                        Connection = connection,
                        CommandText = "PKG_POSTS.GetPostList",
                        CommandType = CommandType.StoredProcedure
                    })
                {
                    try
                    {

                        #region [입력 파라미터]
                        command.Parameters.Add(new OracleParameter("in_limit", OracleDbType.Int32)).Value = Convert.ToInt32(request["limitCnt"].ToString());
                        #endregion

                        #region [리턴 파라미터]
                        command.Parameters.Add(new OracleParameter("out_result_code", OracleDbType.Varchar2, DataLength.out_code)).Direction = ParameterDirection.Output;
                        command.Parameters.Add(new OracleParameter("out_result_message", OracleDbType.Varchar2, DataLength.out_long_msg)).Direction = ParameterDirection.Output;
                        command.Parameters.Add(new OracleParameter("out_result_data", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                        #endregion

                        #region [디비 커넥션 연결] 
                        connection.Open();
                        #endregion

                        #region [디비사용]
                        response.Add("Data", selectListTwo(command));
                        #endregion

                        #region [결과값 세팅]                        
                        response.Add("Result_Code", command.Parameters["out_result_code"].Value.ToString()!);
                        response.Add("Result_Msg", command.Parameters["out_result_message"].Value.ToString()!);
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        #region [에러 메시지 세팅]
                        response.Add("Result_Code", "ERROR");
                        response.Add("Result_Msg", ex.Message);

                        if (LogUtil.ErrLogYn == true)
                        {
                            LogUtil.ErrLogParam("Post-GetPostList", request);
                            LogUtil.ErrLogLine();
                            LogUtil.ErrLogMsg(ex);
                            LogUtil.ErrLogLine();
                        }
                        #endregion
                    }
                }
            }
            return response;
        }
        #endregion

        #region [구현 - 포스트목록조회 3]
        public static Dictionary<string, object> GetPostListThree(ReqGetPostList request)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();

            using (OracleConnection connection = new OracleConnection(DataBaseConf.ConnectionStrings))
            {
                using (
                    OracleCommand command = new OracleCommand
                    {
                        Connection = connection,
                        CommandText = "PKG_POSTS.GetPostList",
                        CommandType = CommandType.StoredProcedure
                    })
                {
                    try
                    {

                        #region [입력 파라미터]
                        command.Parameters.Add(new OracleParameter("in_limit", OracleDbType.Int32)).Value = Convert.ToInt32(request.LimitCnt);
                        #endregion

                        #region [리턴 파라미터]
                        command.Parameters.Add(new OracleParameter("out_result_code", OracleDbType.Varchar2, DataLength.out_code)).Direction = ParameterDirection.Output;
                        command.Parameters.Add(new OracleParameter("out_result_message", OracleDbType.Varchar2, DataLength.out_long_msg)).Direction = ParameterDirection.Output;
                        command.Parameters.Add(new OracleParameter("out_result_data", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                        #endregion

                        #region [디비 커넥션 연결] 
                        connection.Open();
                        #endregion

                        #region [디비사용]
                        response.Add("Data", selectListTwo(command));
                        #endregion

                        #region [결과값 세팅]
                        response.Add("Result_Code", command.Parameters["out_result_code"].Value.ToString()!);
                        response.Add("Result_Msg", command.Parameters["out_result_message"].Value.ToString()!);
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        #region [에러 메시지 세팅]
                        response.Add("Result_Code", "ERROR");
                        response.Add("Result_Msg", ex.Message);

                        if (LogUtil.ErrLogYn == true)
                        {
                            LogUtil.ErrLogParam("Post-GetPostList", request);
                            LogUtil.ErrLogLine();
                            LogUtil.ErrLogMsg(ex);
                            LogUtil.ErrLogLine();
                        }
                        #endregion
                    }
                }
            }
            return response;
        }
        #endregion

        #region [구현 - 포스트목록조회4]
        public static ResGetPostList GetPostListFore(ReqGetPostList request)
        {
            #region [응답객체 초기화 - 세팅]
            ResGetPostList response = new();
            #endregion

            using (OracleConnection connection = new(DataBaseConf.ConnectionStrings))
            {
                using OracleCommand command = new()
                {
                    Connection = connection,
                    CommandText = "PKG_POSTS.GetPostList",
                    CommandType = CommandType.StoredProcedure
                };
                try
                {
                    #region [환경설정 - 초기화]
                    List<Dictionary<string, object>> exeDb = new();
                    #region [모델 초기화 - 세팅]
                    List<MGetPostList> mPostsList = new();
                    MGetPostList obj = new();
                    #endregion
                    #endregion

                    #region [입력 파라미터 - 세팅]
                    command.Parameters.Add(new OracleParameter("in_limit", OracleDbType.Int32)).Value = request.LimitCnt;
                    #endregion

                    #region [리턴 파라미터 - 세팅]
                    command.Parameters.Add(new OracleParameter("out_result_code", OracleDbType.Varchar2, DataLength.out_code)).Direction = ParameterDirection.Output;
                    command.Parameters.Add(new OracleParameter("out_result_message", OracleDbType.Varchar2, DataLength.out_long_msg)).Direction = ParameterDirection.Output;
                    command.Parameters.Add(new OracleParameter("out_result_data", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    #endregion

                    #region [디비 커넥션 연결] 
                    connection.Open();
                    #endregion

                    #region [디비사용 - 다수조회면 selectList]
                    exeDb = selectList(command);

                    #region [객체의 프로퍼티 명 가져오기]
                    Type postsType = obj.GetType();
                    PropertyInfo[] properties = postsType.GetProperties();
                    #endregion

                    foreach (Dictionary<string, object> dictionary in exeDb)
                    {
                        obj = new();
                        foreach (KeyValuePair<string, object> kvp in dictionary)
                        {
                            foreach (PropertyInfo property in properties)
                            {
                                string propertyName = property.Name.ToLower().Replace("_", "");
                                string key = kvp.Key.ToLower().Replace("_", "");
                                object value = kvp.Value;
                                if (propertyName.Equals(key))
                                {
                                    #region[프로퍼티 매핑 - 세팅]
                                    switch (propertyName)
                                    {
                                        /*case "id":
                                            //obj.Id = Convert.ToInt32(value);
                                            break;
                                        case "title":
                                            obj.Title = Convert.ToString(value);
                                            break;
                                        case "createdat":
                                            obj.CreatedAt = MyUtils.ConvertYYYYMMDDHHmmss(Convert.ToDateTime(value));
                                            break;
                                        case "author":
                                            obj.Author = Convert.ToString(value);
                                            break;*/
                                    }
                                    #endregion
                                }
                            }
                        }
                        mPostsList.Add(obj);
                    }

                    response.Data = mPostsList;
                    #endregion

                    #region [결과값 - 세팅]
                    response.Result_Code = command.Parameters["out_result_code"].Value.ToString();
                    response.Result_Msg = command.Parameters["out_result_message"].Value.ToString();
                    #endregion
                }
                catch (Exception ex)
                {
                    #region [에러 메시지]
                    response.Result_Code = "ERROR";
                    response.Result_Msg = ex.Message;

                    if (LogUtil.ErrLogYn == true)
                    {
                        string methodName = MethodBase.GetCurrentMethod()!.Name;
                        LogUtil.ErrLogParam(methodName, request);
                        LogUtil.ErrLogLine();
                        LogUtil.ErrLogMsg(ex);
                        LogUtil.ErrLogLine();
                    }
                    #endregion
                }
            }
            return response;
        }
        #endregion


        #region [구현 - 포스트목록조회5]
        public static ResGetPostList GetPostListFive(ReqGetPostList request)
        {
            #region [응답객체 초기화 - 세팅]
            ResGetPostList response = new();
            #endregion

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

        #endregion

        #region [구현 - 포스트목록상세]
        public static ResGetPostDetail GetPostDetail(ReqGetPostDetail request)
        {
            #region [응답객체 초기화 - 세팅]
            ResGetPostDetail response = new();
            #endregion

            using (OracleConnection connection = new(DataBaseConf.ConnectionStrings))
            {
                using OracleCommand command = new()
                {
                    Connection = connection,
                    CommandText = "PKG_POSTS.GetPostDetail",
                    CommandType = CommandType.StoredProcedure
                };
                try
                {
                    #region [환경설정 - 초기화]                        
                    Dictionary<string, object> exeDb = new();
                    #region [모델 초기화 - 세팅]
                    MGetPostDetail obj = new();
                    #endregion
                    #endregion

                    #region [입력 파라미터 - 세팅]
                    command.Parameters.Add(new OracleParameter("in_id", OracleDbType.Int32)).Value = request.Id;
                    #endregion

                    #region [리턴 파라미터 - 세팅]
                    command.Parameters.Add(new OracleParameter("out_result_code", OracleDbType.Varchar2, DataLength.out_code)).Direction = ParameterDirection.Output;
                    command.Parameters.Add(new OracleParameter("out_result_message", OracleDbType.Varchar2, DataLength.out_long_msg)).Direction = ParameterDirection.Output;
                    command.Parameters.Add(new OracleParameter("out_result_data", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    #endregion

                    #region [디비 커넥션 연결] 
                    connection.Open();
                    #endregion

                    #region [객체의 프로퍼티 명 가져오기]                        
                    Type postsType = obj.GetType();
                    PropertyInfo[] properties = postsType.GetProperties();
                    #endregion

                    #region [디비사용 - 하나만 조회면 selectOne]
                    exeDb = selectOne(command);

                    foreach (PropertyInfo property in properties)
                    {
                        string propertyName = property.Name.ToLower().Replace("_", "");

                        foreach (string key in exeDb.Keys)
                        {
                            object value = exeDb[key];

                            if (propertyName.Equals(key))
                            {
                                #region[프로퍼티 매핑 - 세팅]
                                switch (propertyName)
                                {
                                    case "id":
                                        obj.Id = Convert.ToInt32(value);
                                        break;
                                    case "title":
                                        obj.Title = Convert.ToString(value);
                                        break;
                                    case "content":
                                        obj.Content = Convert.ToString(value);
                                        break;
                                    case "createdat":
                                        obj.CreatedAt = MyUtils.ConvertYYYYMMDDHHmmss(Convert.ToDateTime(value));
                                        break;
                                }
                                #endregion
                            }
                        }
                    }

                    response.Data = obj;
                    #endregion

                    #region [결과값 - 세팅]
                    response.Result_Code = command.Parameters["out_result_code"].Value.ToString();
                    response.Result_Msg = command.Parameters["out_result_message"].Value.ToString();
                    #endregion
                }
                catch (Exception ex)
                {
                    #region [에러 메시지]
                    response.Result_Code = "ERROR";
                    response.Result_Msg = ex.Message;

                    if (LogUtil.ErrLogYn == true)
                    {
                        string methodName = MethodBase.GetCurrentMethod()!.Name;
                        LogUtil.ErrLogParam(methodName, request);
                        LogUtil.ErrLogLine();
                        LogUtil.ErrLogMsg(ex);
                        LogUtil.ErrLogLine();
                    }
                    #endregion
                }
            }
            return response;
        }
        #endregion

        #region [구현 - 포스트 등록]
        public static ResAction AddPost(ReqPost request)
        {
            #region [응답객체 초기화 - 세팅]
            ResAction response = new();
            #endregion

            using (OracleConnection connection = new(DataBaseConf.ConnectionStrings))
            {
                using OracleCommand command = new()
                {
                    Connection = connection,
                    CommandText = "PKG_POSTS.AddPost",
                    CommandType = CommandType.StoredProcedure
                };
                try
                {
                    #region [환경설정 - 초기화]                        
                    Dictionary<string, object> exeDb = new();
                    #region [모델 초기화 - 세팅]
                    MGetPostDetail obj = new();
                    #endregion
                    #endregion

                    #region [입력 파라미터 - 세팅]
                    command.Parameters.Add(new OracleParameter("in_title", OracleDbType.Varchar2)).Value = request.Title;
                    command.Parameters.Add(new OracleParameter("in_content", OracleDbType.Varchar2)).Value = request.Content;
                    command.Parameters.Add(new OracleParameter("in_create_at", OracleDbType.Varchar2)).Value = request.CreatedAt;
                    command.Parameters.Add(new OracleParameter("in_author", OracleDbType.Varchar2)).Value = request.Author;
                    #endregion

                    #region [리턴 파라미터 - 세팅]
                    command.Parameters.Add(new OracleParameter("out_result_code", OracleDbType.Varchar2, DataLength.out_code)).Direction = ParameterDirection.Output;
                    command.Parameters.Add(new OracleParameter("out_result_message", OracleDbType.Varchar2, DataLength.out_long_msg)).Direction = ParameterDirection.Output;
                    #endregion

                    #region [디비 커넥션 연결] 
                    connection.Open();
                    #endregion                   

                    #region [디비사용 - 등록,수정,삭제면 acttionDb]
                    exeDb = acttionDb(command);
                    #endregion

                    #region [결과값 - 세팅]
                    response.Result_Code = command.Parameters["out_result_code"].Value.ToString();
                    response.Result_Msg = command.Parameters["out_result_message"].Value.ToString();
                    #endregion
                }
                catch (Exception ex)
                {
                    #region [에러 메시지]
                    response.Result_Code = "ERROR";
                    response.Result_Msg = ex.Message;

                    if (LogUtil.ErrLogYn == true)
                    {
                        string methodName = MethodBase.GetCurrentMethod()!.Name;
                        LogUtil.ErrLogParam(methodName, request);
                        LogUtil.ErrLogLine();
                        LogUtil.ErrLogMsg(ex);
                        LogUtil.ErrLogLine();
                    }
                    #endregion
                }
            }
            return response;
        }
        #endregion
    }
}

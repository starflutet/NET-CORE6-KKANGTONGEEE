using ApiReperenceServer.Source.DSerialize;
using ApiReperenceServer.Source.Models.Posts;
using ApiReperenceServer.Source.Serialize;
using ApiReperenceServer.Source.Serialize.Posts;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Reflection;
using static ApiReperenceServer.Source.App.Constants;

namespace ApiReperenceServer.Source.Controllers.Posts.Service
{
    public class PostsImpl
    {
        #region [목록을 가져오기위한 반복 메소드]
        public static List<MPosts> selectList(OracleCommand command)
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
                                    obj = setPostObj(propertyName, obj, dataReader.GetValue(i));
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
        public static List<Dictionary<string, object>> selectListTwo(OracleCommand command)
        {
            List<Dictionary<string, object>> resultList = new();

            using (OracleDataReader dataReader = command.ExecuteReader())
            {
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Dictionary<string, object> obj = new Dictionary<string, object>();

                        for (int i = 0; i < dataReader.VisibleFieldCount; i++)
                        {
                            obj.Add(dataReader.GetName(i), dataReader.GetValue(i));
                        }

                        resultList.Add(obj);
                    }
                }
                return resultList;
            }
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
                                    resultDetail = setPostObj(propertyName, resultDetail, dataReader.GetValue(i));
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

        #region [Posts-프로퍼티 세팅 메소드]
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
        #endregion

        #region [구현 - 포스트목록조회]
        public static ResponseGetPostList GetPostList(RequestGetPostList request)
        {
            ResponseGetPostList response = new ResponseGetPostList();

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
                        response.Data = selectList(command);
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
                        response.Add("Result_Code", command.Parameters["out_result_code"].Value.ToString());
                        response.Add("Result_Msg", command.Parameters["out_result_message"].Value.ToString());
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

        #region [구현 - 포스트목록상세]
        public static ResponseGetPostDetail GetPostDetail(RequestGetPostDetail request)
        {
            ResponseGetPostDetail response = new ResponseGetPostDetail();

            using (OracleConnection connection = new OracleConnection(DataBaseConf.ConnectionStrings))
            {
                using (
                    OracleCommand command = new OracleCommand
                    {
                        Connection = connection,
                        CommandText = "PKG_POSTS.GetPostDetail",
                        CommandType = CommandType.StoredProcedure
                    })
                {
                    try
                    {
                        #region [리스트 초기화]
                        List<MPosts> postsList = new List<MPosts>();
                        #endregion

                        #region [입력 파라미터]
                        command.Parameters.Add(new OracleParameter("in_id", OracleDbType.Int32)).Value = request.Id;
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
                        response.Data = selectDetail(command);
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
            }
            return response;
        }
        #endregion


    }
}

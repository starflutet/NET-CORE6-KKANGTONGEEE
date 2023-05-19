using ApiReperenceServer.Source.DSerialize;
using ApiReperenceServer.Source.Models;
using ApiReperenceServer.Source.Serialize;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Data.Common;
using System.Reflection;
using static ApiReperenceServer.Source.App.Constants;

namespace ApiReperenceServer.Source.Controllers.Post.Service
{
    public class PostsImpl
    {
        #region [���� - ����Ʈ�����ȸ]
        public static responsePOSTS GetPostList(requestPOSTS request)
        {
            responsePOSTS response = new responsePOSTS();

            OracleConnection _conn = new OracleConnection(DataBaseConf.ConnectionStrings);

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
                        #region [����Ʈ �ʱ�ȭ]
                        List<Posts> postsList = new List<Posts>();
                        #endregion

                        #region [�Է� �Ķ����]
                        command.Parameters.Add(new OracleParameter("in_limit", OracleDbType.Int32)).Value = request.LimitCnt;
                        #endregion

                        #region [���� �Ķ����]
                        command.Parameters.Add(new OracleParameter("out_result_code", OracleDbType.Varchar2, DataLength.out_code)).Direction = ParameterDirection.Output;
                        command.Parameters.Add(new OracleParameter("out_result_message", OracleDbType.Varchar2, DataLength.out_long_msg)).Direction = ParameterDirection.Output;
                        command.Parameters.Add(new OracleParameter("out_result_data", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                        #endregion

                        #region [��� Ŀ�ؼ� ����] 
                        connection.Open();
                        #endregion

                        #region [�����]
                        using (OracleDataReader dataReader = command.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    postsList.Add(selectList(dataReader));
                                }
                            }
                        }
                        #endregion

                        #region [����� ����]
                        response.Data = postsList;
                        response.Result_Code = command.Parameters["out_result_code"].Value.ToString();
                        response.Result_Msg = command.Parameters["out_result_message"].Value.ToString();
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        #region [���� �޽��� ����]
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

        #region [����� ������������ �ݺ� �޼ҵ�]
        public static Posts selectList(OracleDataReader dataReader)
        {
            Posts obj = new Posts();
            #region [��ü�� ������Ƽ �� ��������]
            Type postsType = typeof(Posts);
            PropertyInfo[] properties = postsType.GetProperties();
            #endregion

            #region [���θ�Ƽ ������ �ݺ�����]
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
            return obj;
        }
        #endregion

        #region [Posts-������Ƽ ���� �޼ҵ�]
        public static Posts setPostObj(string propertiesName, Posts obj, object value)
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
    }
}

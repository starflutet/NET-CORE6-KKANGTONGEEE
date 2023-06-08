using Oracle.ManagedDataAccess.Client;
using System;

namespace ApiReperenceServer.Source.App
{
    public class Constants
    {
        #region [디비 환경설정]
        public class DataBaseConf
        {
            #region [호스트] 
            private const string Host = "localhost";
            #endregion

            #region [포트]
            private const string Port = "1521";
            #endregion

            #region [서비스이름]
            private const string ServiceName = "xe";
            #endregion

            #region [사용자 아이디]
            private const string UserId = "SYSTEM";
            #endregion

            #region [사용자 비밀번호]
            private const string UserPw = "1234";
            #endregion

            #region [커넥션 스트링]
            public static string ConnectionStrings = string.Format("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1}))(CONNECT_DATA=(SERVICE_NAME={2})));User ID={3};Password={4};", Host, Port, ServiceName, UserId, UserPw);
            #endregion
        }
        #endregion

        #region [디비 길이 환경설정]
        public class DataLength
        {
            #region [출력코드 길이]
            public const int out_code = 128;
            #endregion
            #region [출력메시지 길이]
            public const int out_long_msg = 128;
            #endregion
        }
        #endregion

        public class LogUtil
        {
            #region [에러 로그 사용여부]
            public const bool ErrLogYn = true;
            #endregion

            #region [에러 로그 라인]
            private const string ErrLine = "====================================================";
            #endregion

            public static void ErrLogLine()
            {
                System.Diagnostics.Debug.WriteLine(ErrLine);
            }

            public static void ErrLogParam(string param, object request)
            {
                System.Diagnostics.Debug.WriteLine(param, "에러 메소드명(:파라미터) ::: " + request);
            }

            public static void ErrLogMsg(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}

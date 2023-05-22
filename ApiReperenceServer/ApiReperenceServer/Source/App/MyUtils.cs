namespace ApiReperenceServer.Source.App
{
    public class MyUtils
    {
        #region[날짜 데이트포멧 변환 - 년월일시분초]
        public static string ConvertYYYYMMDDHHmmss(DateTime dateTime)
        {
            return dateTime.ToString("yyyy.MM.dd HH:mm:ss");
        }
        #endregion
    }
}

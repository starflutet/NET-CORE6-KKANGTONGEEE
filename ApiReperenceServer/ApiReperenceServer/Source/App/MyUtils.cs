namespace ApiReperenceServer.Source.App
{
    public class MyUtils
    {
        /// <summary>
        /// 날짜 데이트포멧 변환 - 년월일시분초
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ConvertYYYYMMDDHHmmss(DateTime dateTime)
        {
            return dateTime.ToString("yyyy.MM.dd HH:mm:ss");
        }

        /// <summary>
        /// UUID로 12자리 생성
        /// </summary>
        /// <returns></returns>
        public static string GenerateToken()
        {
            Guid guid = Guid.NewGuid(); // UUID 생성
            string uuidString = guid.ToString("N"); // 하이픈 제외한 UUID 문자열

            string token = uuidString[..16]; // 16자리로 제한

            return token;
        }
    }
}

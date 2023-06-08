using System.Text.Json;

public class LoginInterCeptor
{
    private readonly RequestDelegate _next;

    public LoginInterCeptor(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/UserControllers"))
        {
            await _next(context);
        }
        else
        {
            if (context.Session.GetString("LoginChk") == null)
            {
                context.Response.ContentType = "application/json";

                var response = new
                {
                    status = -1,
                    message = "로그인이 필요합니다."
                };

                #region [ JSON 형식으로 응답값 반환 ]
                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
                #endregion
                return;
            }
            else
            {
                await _next(context);
            }
        }
    }
}

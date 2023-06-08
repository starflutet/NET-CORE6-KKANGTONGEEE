
using Castle.DynamicProxy;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiReperenceServer Doc", Version = "0.0.1v" });
});

//1. 세션등록
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1800);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyApiReperenceServer Doc 0.0.1v");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//2. 세션 사용
app.UseSession();

// 미들웨어(인터셉터 등록)
app.UseMiddleware<LoginInterCeptor>();

app.Run();



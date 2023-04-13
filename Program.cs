using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using MySql.EntityFrameworkCore.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Forum;
using Forum.Entities;
using Forum.Helpers;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Services.AddControllers();


// 配置数据库
if (configuration.GetConnectionString("MySqlConnection") == null)
    throw new InvalidOperationException("您没有配置MySqlConnection连接信息");
var mysqlConnectionString = configuration.GetConnectionString("MySqlConnection")!;
builder.Services.AddDbContext<ForumDb>(options => options.UseMySQL(mysqlConnectionString));
if (configuration.GetConnectionString("RedisConnection") == null)
    throw new InvalidOperationException("您没有配置RedisConnection连接信息");
var redisConnectionString = configuration.GetConnectionString("RedisConnection")!;
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));
builder.Services.AddSingleton<RedisHelper>();


// 配置存储桶
if (configuration["QCos:Secret_Id"] == null)
    throw new InvalidOperationException("您没有配置cos存储桶的连接信息");
builder.Services.AddSingleton<QCosHelper>(new QCosHelper(
    configuration["QCos:Secret_Id"]!, configuration["QCos:Secret_Key"]!, configuration["QCos:Bucket"]!,
    configuration["QCos:Region"]!, configuration["QCos:Prefix"]!
));


// 配置JWT
if (configuration["Jwt:Issuer"] == null)
    Console.WriteLine("注意：您没有配置Jwt相关信息。程序会使用默认的配置，但这是极不安全的。");
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = configuration["Jwt:Issuer"] ?? "SampleIssuer",
            ValidateAudience = true,
            ValidAudience = configuration["Jwt:Audience"] ?? "SampleAudience",
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    configuration["Jwt:SecretKey"] ?? "SampleKey")),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(1),
            RequireExpirationTime = true,
        };
    });
builder.Services.AddSingleton(new JwtHelper(configuration));


// 配置Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "同济大学高程论坛后端API",
        Description = "高程答疑新时代",
        Version = "v1"
    });
});


// 配置授权
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RegUsr", policy => policy.RequireClaim(ClaimTypes.Name));

    options.AddPolicy("Stu", policy => policy.RequireClaim(ClaimTypes.Role,
        StudentRole.Normal, StudentRole.TA, StudentRole.Admin, StudentRole.Super));

    options.AddPolicy("TA", policy => policy.RequireClaim(ClaimTypes.Role,
        StudentRole.TA, StudentRole.Admin, StudentRole.Super));

    options.AddPolicy("Adm", policy => policy.RequireClaim(ClaimTypes.Role,
        StudentRole.Admin, StudentRole.Super));

    options.AddPolicy("SU", policy => policy.RequireClaim(ClaimTypes.Role,
        StudentRole.Super));
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "同济大学高程论坛后端API");
});

app.MapGet("/", () => "This Service Do Not Offer An Frontend Interface.");

app.MapControllers();

app.Run();

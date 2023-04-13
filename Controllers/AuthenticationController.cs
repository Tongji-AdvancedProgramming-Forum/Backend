using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

using Forum.Helpers;
using Forum.Services;
using Forum.DTO.StudentService;
using Forum.Entities;
using Microsoft.AspNetCore.Http;

namespace Forum.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly JwtHelper _jwtHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ForumDb _db;
        private readonly RedisHelper _redis;
        private StudentService service;

        public AuthenticationController(JwtHelper jwtHelper, ForumDb db, IHttpContextAccessor httpContextAccessor, RedisHelper redis)
        {
            _jwtHelper = jwtHelper;
            _httpContextAccessor = httpContextAccessor;
            _db = db;
            _redis = redis;
            service = new StudentService(_db, _redis);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var student = await service.Login(_httpContextAccessor.HttpContext!, request);
                if (student == null)
                {
                    return Unauthorized(new LoginResponse
                    {
                        Success = false,
                        Message = "用户名或密码错误"
                    });
                }
                var token = _jwtHelper.CreateToken(student.stuno, student.level,
                       Math.Min(request.ValidityPeriod, 10080));
                return Accepted(new LoginResponse
                {
                    Success = true,
                    Message = "登录成功",
                    Token = token,
                    UserData = student
                });
            }
            catch (Exception e)
            {
                return Ok(new LoginResponse
                {
                    Success = false,
                    Message = "内部错误：" + e.Message
                });
            }
        }

        [Authorize]
        [HttpGet("test")]
        public IActionResult AuthTest()
        {
            var identity = _httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
            var name = identity?.FindFirst(ClaimTypes.Name)?.Value ?? "";

            return Ok($"{name}已成功验证");
        }
    }
}


using Microsoft.EntityFrameworkCore;
using Forum.Entities;
using Forum.DTO.StudentService;

namespace Forum.Service
{
    public class StudentService
    {
        private ForumDb db = null!;

        public StudentService(ForumDb dbContext)
        {
            this.db = dbContext;
        }

        protected async Task<Student?> LoginAuth(string UserName, string PassWord)
        {
            var result = await db.student.Where(s => (1 == 1
                && s.StuNo == UserName
                && s.Password == PassWord
                && s.Enable == "1"
                && s.IsDel == "0"
               )).ToListAsync();
            return result.Count == 1 ? result.First() : null;
        }

        public async Task<LoginResponse> Login(HttpContext httpContext, LoginRequest request)
        {
            var IPAddr = httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            var Agent = httpContext.Request.Headers["User-Agent"].ToString();

            Student? student = await LoginAuth(request.UserName, request.PassWord);
            if (student == null)
            {
                if (request.UserName != null && request.UserName.Length > 0)
                {
                    await db.WriteLogLoginAsync(request.UserName, IPAddr, Agent, "登录失败");
                }
                return new LoginResponse
                {
                    Success = false,
                    Message = "用户名或密码错误"
                };
            }

            await db.WriteLogLoginAsync(request.UserName, IPAddr, Agent, "登录成功");
            return new LoginResponse
            {
                Success = true,
                Token = "",
                UserData = new
                {

                }
            };
        }
    }
}
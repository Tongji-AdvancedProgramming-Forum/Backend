using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

using Forum.Helpers;
using Forum.Entities;
using Forum.DTO.StudentService;

namespace Forum.Services
{
    public class StudentService
    {
        private readonly ForumDb _db;

        private readonly RedisHelper _redis;

        public StudentService(ForumDb dbContext, RedisHelper redis)
        {
            _db = dbContext;
            _redis = redis;
        }

        protected async Task<Student?> LoginAuth(string UserName, string PassWord)
        {
            var result = await _db.student.Where(s => (1 == 1
                && s.StuNo == UserName
                && s.Password == MD5Helper.CalculateMD5Hash(PassWord)
                && s.Enable == "1"
                && s.IsDel == "0"
               )).ToListAsync();
            return result.Count == 1 ? result.First() : null;
        }

        /// <summary>
        /// 实践中，头像的键为【AVA_UserName】。
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public async Task<string> GetAvatar(string UserName)
        {
            var db = _redis.GetDatabase(1);
            var key = "AVA_" + UserName;
            var res = await db.StringGetAsync(key);
            return res.ToString();  // 你也许会觉得不进行null check似乎不妥，但是redis会为不存在的键返回""
        }

        /// <summary>
        /// 实践中，头像的键为【AVA_UserName】。
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public async Task SetAvatar(string UserName, string url)
        {
            var db = _redis.GetDatabase(1);
            await db.StringSetAsync("AVA_" + UserName, url);
        }

        public async Task<StudentInfoDTO?> Login(HttpContext httpContext, LoginRequest request)
        {
            var IPAddr = httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            var Agent = httpContext.Request.Headers["User-Agent"].ToString();

            Student? student = await LoginAuth(request.UserName, request.PassWord);
            if (student == null)
            {
                if (request.UserName != null && request.UserName.Length > 0)
                {
                    try
                    {
                        await _db.WriteLogLoginAsync(request.UserName, IPAddr, Agent, "登录失败");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                return null;
            }

            await _db.WriteLogLoginAsync(request.UserName, IPAddr, Agent, "登录成功");

            return new StudentInfoDTO
            {
                term = student.Term,
                grade = student.Grade,
                stuno = student.StuNo,
                name = student.Name,
                sex = student.Sex,
                avatar = await GetAvatar(student.Name),
                class_fname = student.ClassFullName,
                class_sname = student.ClassShortName,
                level = student.UserLevel,
                cno = student.CourseNo_1 ?? ""
            };
        }
    }
}
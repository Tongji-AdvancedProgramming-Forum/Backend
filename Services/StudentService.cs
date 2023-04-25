using Microsoft.EntityFrameworkCore;
using System.Web;


using Forum.Helpers;
using Forum.Entities;
using Forum.DTO.StudentService;

namespace Forum.Services
{
    public class StudentService
    {
        private readonly ForumDb _db;
        private readonly RedisHelper _redis;
        private readonly IConfiguration _configuration;

        public StudentService(ForumDb dbContext, RedisHelper redis, IConfiguration configuration)
        {
            _db = dbContext;
            _redis = redis;
            _configuration = configuration;
        }

        private async Task AddStudentIfNotExists(Student student)
        {
            if (await _db.student.FindAsync(student.StuNo) == null)
            {
                await _db.student.AddAsync(student);
                await _db.SaveChangesAsync();
            }
        }

        private async Task<Student?> LoginAuth(string userName, string passWord)
        {
            var result = await _db.student.Where(s => (1 == 1
                && s.StuNo == userName
                && s.Password == MD5Helper.CalculateMD5Hash(passWord)
                && s.Enable == "1"
                && s.IsDel == "0"
               )).ToListAsync();
            return result.Count == 1 ? result.First() : null;
        }

        private async Task<Student?> LoginAuthRemote(string userName, string passWord)
        {
            var baseUrl = _configuration.GetConnectionString("MicroServiceConnection")!;
            var uriBuilder = new UriBuilder(baseUrl + "loginauth");

            var queryParameters = HttpUtility.ParseQueryString(uriBuilder.Query);
            queryParameters["stuno"] = userName;
            queryParameters["stupw"] = passWord;

            uriBuilder.Query = queryParameters.ToString();
            string url = uriBuilder.ToString();

            using var httpclient = new HttpClient();
            HttpResponseMessage response = await httpclient.PostAsync(url, null);
            response.EnsureSuccessStatusCode();
            var res = await response.Content.ReadFromJsonAsync<RemoteAuthDTO>();

            if (res == null || !res.success || res.data == null)
            {
                return null;
            }
            else
            {
                var student = new Student
                {
                    Name = res.data.stu_name,
                    StuNo = res.data.sno,
                    Enable = res.data.enable,
                    ClassFullName = res.data.fname,
                    ClassShortName = res.data.sname,
                    Grade = res.data.grade,
                    CourseNo_1 = res.data.tcno,
                    UserLevel = res.data.level,
                    Sex = res.data.sex,
                    Term = res.data.term,
                    CourseNo_1_IsDel = "0",
                    CourseNo_2_IsDel = "0",
                    CourseNo_3_IsDel = "0",
                    IsDel = "0"
                };
                await AddStudentIfNotExists(student);
                return student;
            }
        }

        /// <summary>
        /// 实践中，头像的键为【AVA_UserName】。
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<string> GetAvatar(string userName)
        {
            var db = _redis.GetDatabase(1);
            var key = "AVA_" + userName;
            var res = await db.StringGetAsync(key);
            return res.ToString();  // 你也许会觉得不进行null check似乎不妥，但是redis会为不存在的键返回""
        }

        /// <summary>
        /// 实践中，头像的键为【AVA_UserName】。
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task SetAvatar(string userName, string url)
        {
            var db = _redis.GetDatabase(1);
            await db.StringSetAsync("AVA_" + userName, url);
        }

        public async Task<StudentInfoDTO?> Login(HttpContext httpContext, LoginRequest request)
        {
            var ipAddr = httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            var agent = httpContext.Request.Headers["User-Agent"].ToString();

            Student? student;
            if (_configuration["LoginAuthMethod"] == "remote")
            {
                student = await LoginAuthRemote(request.UserName, request.PassWord);
            }
            else
            {
                student = await LoginAuth(request.UserName, request.PassWord);
            }

            if (student == null)
            {
                if (request.UserName.Length > 0)
                {
                    try
                    {
                        await _db.WriteLogLoginAsync(request.UserName, ipAddr, agent, "登录失败");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                return null;
            }

            await _db.WriteLogLoginAsync(request.UserName, ipAddr, agent, "登录成功");

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
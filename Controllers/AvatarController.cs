using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

using Forum.Helpers;
using Forum.Services;
using Forum.DTO.StudentService;
using Forum.Entities;

namespace Forum.Controllers
{
    [Route("api/stu/[controller]")]
    [ApiController]
    public class AvatarController : ControllerBase
    {
        private readonly JwtHelper _jwtHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ForumDb _db;
        private readonly RedisHelper _redis;
        private readonly QCosHelper _qcos;
        private StudentService stuService;

        public AvatarController(JwtHelper jwtHelper, ForumDb db, IHttpContextAccessor httpContextAccessor,
                                    RedisHelper redis, QCosHelper qcos)
        {
            _jwtHelper = jwtHelper;
            _httpContextAccessor = httpContextAccessor;
            _db = db;
            _redis = redis;
            _qcos = qcos;
            stuService = new StudentService(_db, _redis);
        }

        /// <summary>
        /// 获取或批量获取头像
        /// </summary>
        /// <param name="type">获取类型：默认为0，表示自己；1表示给定一个uid；2表示给定一组uid</param>
        /// <param name="requested">type为1时有效。需要查询的id</param>
        /// <param name="requests">type为2时有效。需要查询的id组</param>
        /// <returns></returns>
        [Authorize(Policy = "RegUsr")]
        [HttpGet]
        public async Task<IActionResult> GetAvatar(string type = "0", string requested = "", [FromQuery] IList<string> requests = null!)
        {
            var identity = _httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
            var stuName = identity?.FindFirst(ClaimTypes.Name)?.Value ?? "";

            switch (type)
            {
                case "0":
                    {
                        return Ok(new
                        {
                            url = await stuService.GetAvatar(stuName)
                        });
                    }
                case "1":
                    {
                        if (requested == null || requested == "")
                            return BadRequest();
                        return Ok(new
                        {
                            url = await stuService.GetAvatar(requested)
                        });
                    }
                case "2":
                    {
                        if (requests == null || requests.Count == 0)
                            return BadRequest();
                        var response = new List<Object>();
                        foreach (string rid in requests)
                        {
                            response.Add(new
                            {
                                stuId = rid,
                                url = await stuService.GetAvatar(rid)
                            });
                        }
                        return Ok(response);
                    }
                default:
                    return BadRequest();
            }
        }

        /// <summary>
        /// 设置头像而无需上传
        /// </summary>
        /// <param name="avatarUrl">头像图片的Url</param>
        /// <returns></returns>
        [Authorize(Policy = "RegUsr")]
        [HttpPost]
        public async Task<IActionResult> SetAvatar(string avatarUrl)
        {
            var identity = _httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
            var stuName = identity?.FindFirst(ClaimTypes.Name)?.Value ?? "";

            await stuService.SetAvatar(stuName, avatarUrl);

            return Ok();
        }

        /// <summary>
        /// 上传头像
        /// </summary>
        /// <param name="file">头像文件</param>
        /// <returns></returns>
        [Authorize(Policy = "RegUsr")]
        [HttpPut]
        public async Task<IActionResult> PutAvatar(IFormFile file)
        {
            var identity = _httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
            var stuName = identity?.FindFirst(ClaimTypes.Name)?.Value ?? "";

            if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty or not provided.");
            }

            var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Temp", "uploads");
            if (!Directory.Exists(uploadFolderPath))
            {
                Directory.CreateDirectory(uploadFolderPath);
            }

            var filePath = Path.Combine(uploadFolderPath, file.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var newFileName = Guid.NewGuid().ToString() + ".jpeg";
            var newFilePath = Path.Combine(uploadFolderPath, newFileName);
            var cosFilePath = "avatars/" + newFileName;

            try
            {
                await ImageHelper.ResizeImage(filePath, newFilePath, 512);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            var uploadRes = await _qcos.Upload(cosFilePath, newFilePath);

            if (uploadRes == null)
            {
                throw new Exception("服务器内部网络错误，请稍候重试");
            }

            await stuService.SetAvatar(stuName, uploadRes);

            return Ok(uploadRes);
        }

    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Forum.Helpers;
using Forum.Entities;

namespace Forum.Controllers
{
    /// <summary>
    /// 在开发过程中使用的API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DevController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly RedisHelper _redis;
        private readonly ForumDb _db;

        public DevController(IWebHostEnvironment env, RedisHelper redis, ForumDb db)
        {
            _env = env;
            _redis = redis;
            _db = db;
        }

        [HttpGet("testrds")]
        public async Task<IActionResult> testRedis()
        {
            if (!_env.IsDevelopment()) return Forbid();
            var res = await _redis.GetDatabase().StringGetAsync("heyhey");
            return Ok(res.ToString());
        }

        [HttpGet("testsql")]
        public async Task<IActionResult> testSQL()
        {
            if (!_env.IsDevelopment()) return Forbid();
            var res = await _db.term.ToListAsync();
            return Ok(res);
        }

    }
}
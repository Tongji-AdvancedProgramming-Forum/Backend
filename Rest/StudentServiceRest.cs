using Forum.Entity;
using Forum.Service;
using Forum.DTO.StudentService;

namespace Forum.Rest
{
    public static class StudentServiceRest
    {
        public static async Task<LoginResponse> Login(ForumDb db,HttpContext httpContext,LoginRequest request)
        {
            var service = new StudentService(db);
            try
            {
                await service.Login(httpContext,request);
            }
            catch (Exception e)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "内部错误："+e.Message
                };
            }
        }
    }
}
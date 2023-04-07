using Forum.Entity;
using Microsoft.EntityFrameworkCore;

namespace Forum.Rest
{
    public static class Rest_Register
    {
        public static void Register(WebApplication app)
        {
            app.MapGet("/", () => "This Service Do Not Offer An Frontend Interface.");
            
            RegisterStudentService(app);
        }

        private static void RegisterStudentService(WebApplication app)
        {

        }

    }
}
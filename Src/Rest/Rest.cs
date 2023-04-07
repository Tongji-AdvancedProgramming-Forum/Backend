using Forum.Entity;
using Microsoft.EntityFrameworkCore;

namespace Forum.Rest
{
    public static class Rest_Register
    {
        public static void Register(WebApplication app)
        {
            app.MapGet("/", () => "This Service Do Not Offer An Frontend Interface.");
            
            RegisterUserService(app);
        }

        private static void RegisterUserService(WebApplication app)
        {
            app.MapGet("/hw", User_Service.HelloWorld);
        }

    }
}
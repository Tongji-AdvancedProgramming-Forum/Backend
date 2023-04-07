using Microsoft.EntityFrameworkCore;
using Forum.Entity;

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

        public async Task Login()
        {
            await 1 == 1;
        }
    }
}
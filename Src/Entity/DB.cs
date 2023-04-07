using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace Forum.Entity
{
    public class ForumDb : DbContext
    {
        public ForumDb(DbContextOptions options) : base(options) { }

        public DbSet<Term> term { get; set; } = null!;
        public DbSet<Course> course { get; set; } = null!;
        public DbSet<Student> student { get; set; } = null!;
        public DbSet<LogLogin> log_login { get; set; } = null!;
        public DbSet<LogResetPassword> log_rstpwd { get; set; } = null!;
        public DbSet<HomeworkUploaded> homework_uploaded { get; set; } = null!;
        public DbSet<LogHomeworkUploaded> log_homework_uploaded { get; set; } = null!;
        public DbSet<Homework> homework { get; set; } = null!;
        public DbSet<LogHomework> log_homework { get; set; } = null!;
        public DbSet<Tag> tag { get; set; } = null!;
        public DbSet<Post> post { get; set; } = null!;
        public DbSet<LogPost> log_post { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Term.configureDb(modelBuilder);
            Course.configureDb(modelBuilder);
            Student.configureDb(modelBuilder);
            LogLogin.configureDb(modelBuilder);
            LogResetPassword.configureDb(modelBuilder);
            HomeworkUploaded.configureDb(modelBuilder);
            LogHomeworkUploaded.configureDb(modelBuilder);
            Homework.configureDb(modelBuilder);
            LogHomework.configureDb(modelBuilder);
            Tag.configureDb(modelBuilder);
            Post.configureDb(modelBuilder);
            LogPost.configureDb(modelBuilder);
        }

        /// <summary>
        /// 登录日志记录
        /// </summary>
        /// <param name="sno">学号</param>
        /// <param name="ipAddr">IP地址</param>
        /// <param name="userAgent">UA</param>
        /// <param name="comment">信息，例如登录成功/失败</param>
        /// <returns>是否成功(1=成功)</returns>
        public async Task<int> WriteLogLoginAsync(string sno, string ipAddr, string userAgent, string comment)
        {
            var snoParam = new MySqlParameter("@in_sno", sno);
            var ipAddrParam = new MySqlParameter("@in_ipaddr", ipAddr);
            var userAgentParam = new MySqlParameter("@in_useragent", userAgent);
            var commentParam = new MySqlParameter("@in_comment", comment);

            int result = await Database.ExecuteSqlRawAsync("CALL proc_writelog_login(@in_sno, @in_ipaddr, @in_useragent, @in_comment)",
                snoParam, ipAddrParam, userAgentParam, commentParam);
            return result;
        }

        /// <summary>
        /// 上传作业文件日志记录
        /// </summary>
        /// <param name="opno">操作人学号</param>
        /// <param name="ipaddr">IP地址</param>
        /// <param name="comment">备注</param>
        /// <returns>是否成功(1=成功)</returns>
        public async Task<int> WriteLogHomeworkUploadedAsync(string opno, string ipaddr, string comment)
        {
            var opnoParam = new MySqlParameter("@in_opno", opno);
            var ipaddrParam = new MySqlParameter("@in_ipaddr", ipaddr);
            var commentParam = new MySqlParameter("@in_comment", comment);

            int result = await Database.ExecuteSqlRawAsync("CALL proc_writelog_homework_uploaded(@in_opno, @in_ipaddr, @in_comment)", opnoParam, ipaddrParam, commentParam);
            return result;
        }

        /// <summary>
        /// 导入作业日志记录
        /// </summary>
        /// <param name="opno">操作人学号</param>
        /// <param name="ipaddr">IP地址</param>
        /// <param name="comment">备注</param>
        /// <returns>是否成功(1=成功)</returns>
        public async Task<int> WriteLogHomeworkAsync(string opno, string ipaddr, string comment)
        {
            var opnoParam = new MySqlParameter("@in_opno", opno);
            var ipaddrParam = new MySqlParameter("@in_ipaddr", ipaddr);
            var commentParam = new MySqlParameter("@in_comment", comment);

            int result = await Database.ExecuteSqlRawAsync("CALL proc_writelog_homework(@in_opno, @in_ipaddr, @in_comment)", opnoParam, ipaddrParam, commentParam);
            return result;
        }
    }
}
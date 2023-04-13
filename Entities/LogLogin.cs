using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Forum.Entities
{
    /*****************************************************************
    用户登录日志
        log_login_id        - 序号(主键,自动增长)
        log_login_no        - 学号
        log_login_ipaddr    - 登录IP
        log_login_date      - 登录时间
        log_login_useragent - 登录环境(浏览器的agent)
        log_login_comment   - 备注
    *****************************************************************/
    /// <summary>
    /// 用户登录日志
    /// </summary>
    public class LogLogin
    {
        /// <summary>
        /// 序号(主键,自动增长)
        /// </summary>
        [Key]
        [Column("log_login_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 学号
        /// </summary>
        [Required]
        [Column("log_login_no", TypeName = "char(8)")]
        public string StuNo { get; set; } = "";

        /// <summary>
        /// 登录IP
        /// </summary>
        [Column("log_login_ipaddr", TypeName = "char(15)")]
        public string? IPAddr { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        [Column("log_login_date", TypeName = "datetime")]
        public DateTime? Date { get; set; }

        /// <summary>
        /// 登录环境(浏览器的agent)
        /// </summary>
        [Column("log_login_useragent", TypeName = "text")]
        public string? UserAgent { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("log_login_comment", TypeName = "text")]
        public string? Comment { get; set; }

        /// <summary>
        /// 导航属性 - 所属学生
        /// </summary>
        public Student student { get; set; } = null!;

        /// <summary>
        /// 配置数据库关系
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder实例</param>
        public static void configureDb(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogLogin>()
                .HasOne(l => l.student)
                .WithMany()
                .HasForeignKey(l => l.StuNo)
                .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Forum.Entity
{
    /// <summary>
    /// 重置密码日志
    /// </summary>
    public class LogResetPassword
    {
        /// <summary>
        /// 序号(主键,自动增长)
        /// </summary>
        [Key]
        [Column("log_rstpwd_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 申请重置人的学号
        /// </summary>
        [Required]
        [Column("log_rstpwd_no", TypeName = "char(8)")]
        public string StuNo { get; set; } = "";

        /// <summary>
        /// 操作员学号
        /// </summary>
        [Required]
        [Column("log_rstpwd_opno", TypeName = "char(8)")]
        public string OperatorNo { get; set; } = "";

        /// <summary>
        /// 登录IP
        /// </summary>
        [Column("log_rstpwd_ipaddr", TypeName = "char(15)")]
        public string? IPAddr { get; set; }

        /// <summary>
        /// 重置时间
        /// </summary>
        [Column("log_rstpwd_date", TypeName = "datetime")]
        public DateTime? DateTime { get; set; }

        /// <summary>
        /// 导航属性 - 重置密码学生
        /// </summary>
        public Student student { get; set; } = null!;

        /// <summary>
        /// 导航属性 - 操作员
        /// </summary>
        public Student op { get; set; } = null!;

        /// <summary>
        /// 配置数据库关系
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder实例</param>
        public static void configureDb(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogResetPassword>()
                .HasOne(l => l.student)
                .WithMany()
                .HasForeignKey(l => l.StuNo)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<LogResetPassword>()
                .HasOne(l => l.op)
                .WithMany()
                .HasForeignKey(l => l.OperatorNo)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

}
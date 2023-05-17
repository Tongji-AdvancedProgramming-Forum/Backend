using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Entities
{
    /// <summary>
    /// 管理员上传文件日志
    /// </summary>
    public class LogHomeworkUploaded
    {
        /// <summary>
        /// 序号(主键,自动增长)
        /// </summary>
        [Key]
        [Column("log_hwup_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 操作人学号
        /// </summary>
        [Required]
        [Column("log_hwup_opno", TypeName = "char(8)")]
        public string OperatorNo { get; set; } = "";

        /// <summary>
        /// 登录IP
        /// </summary>
        [Column("log_hwup_ipaddr", TypeName = "char(15)")]
        public string? IPAddr { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        [Column("log_hwup_date", TypeName = "datetime")]
        public DateTime? Date { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("log_hwup_comment", TypeName = "text")]
        public string? Comment { get; set; }

        /// <summary>
        /// 导航属性 - 上传者
        /// </summary>
        public Student uploader { get; set; } = null!;

        /// <summary>
        /// 配置数据库关系
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder实例</param>
        public static void ConfigureDb(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogHomeworkUploaded>()
                .HasOne(l => l.uploader)
                .WithMany()
                .HasForeignKey(l => l.OperatorNo)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
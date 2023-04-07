using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Entity
{
    /// <summary>
    /// 发帖日志
    /// 帖子发出后，同时写入日志一份，运行对帖子进行修改，修改后界面显示变化的内容，但是日志是两条
    /// </summary>
    public class LogPost
    {
        /// <summary>
        /// 序号(主键,自动增长)
        /// </summary>
        [Key]
        [Column("log_post_id", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 帖子Id
        /// </summary>
        [Column("log_post_postid", TypeName = "int")]
        public int? PostId { get; set; }

        /// <summary>
        /// 操作人学号
        /// </summary>
        [Required]
        [Column("log_post_opno", TypeName = "char(8)")]
        public string OperatorNo { get; set; } = "";

        /// <summary>
        /// 登录IP
        /// </summary>
        [Column("log_post_ipaddr", TypeName = "char(15)")]
        public string? IPAddr { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        [Column("log_post_date", TypeName = "datetime")]
        public DateTime? DateTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("log_post_comment", TypeName = "text")]
        public string? Comment { get; set; }

        /// <summary>
        /// 导航属性 - 所发表帖子
        /// </summary>
        public Post post { get; set; } = null!;

        /// <summary>
        /// 导航属性 - 学生
        /// </summary>
        public Student op { get; set; } = null!;

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

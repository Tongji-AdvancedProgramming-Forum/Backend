using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Entities
{
    /// <summary>
    /// 管理员导入作业日志
    /// </summary>
    [Table("log_homework")]
    public class LogHomework
    {
        /// <summary>
        /// 序号(主键,自动增长)
        /// </summary>
        [Key]
        [Column("log_hw_id", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 操作人学号
        /// </summary>
        [Required]
        [Column("log_hw_opno", TypeName = "char(8)")]
        public string OperatorNo { get; set; } = "";

        /// <summary>
        /// 登录IP
        /// </summary>
        [Column("log_hw_ipaddr", TypeName = "char(15)")]
        public string? IPAddr { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        [Column("log_hw_date", TypeName = "datetime")]
        public DateTime? DateTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("log_hw_comment", TypeName = "text")]
        public string? Comment { get; set; }

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
            modelBuilder.Entity<LogHomework>()
                .HasOne(l => l.op)
                .WithMany()
                .HasForeignKey(l => l.OperatorNo)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Forum.Entity
{
    /// <summary>
    /// 上传作业信息表
    /// </summary>
    public class HomeworkUploaded
    {
        /// <summary>
        /// 学期(主键+外键)
        /// </summary>
        [Required]
        [Column("hwup_term", TypeName = "char(5)")]
        public string Term { get; set; } = "";

        /// <summary>
        /// 课程序号(主键,对应course中的course_code 注意:不是外键关系,但要检查是否存在)
        /// </summary>
        [Required]
        [Column("hwup_ccode", TypeName = "char(16)")]
        public string CCode { get; set; } = "";

        /// <summary>
        /// 文件编号(例: 22232-030101-W0102)
        /// </summary>
        [Required]
        [Column("hwup_id", TypeName = "char(20)")]
        public string Id { get; set; } = "";

        /// <summary>
        /// 布置周
        /// </summary>
        [Required]
        [Column("hwup_week", TypeName = "tinyint")]
        public byte Week { get; set; }

        /// <summary>
        /// 章节(0-20: 第0-20章 90:大作业 98:文档作业 99:其它作业)
        /// </summary>
        [Required]
        [Column("hwup_chapter", TypeName = "tinyint")]
        public byte Chapter { get; set; }

        /// <summary>
        /// 上传的文件名
        /// </summary>
        [Required]
        [Column("hwup_filename", TypeName = "char(64)")]
        public string FileName { get; set; } = "";

        /// <summary>
        /// 上传的文件的MD5
        /// </summary>
        [Required]
        [Column("hwup_filemd5", TypeName = "char(32)")]
        public string FileMD5 { get; set; } = "";

        /// <summary>
        /// 作业描述
        /// </summary>
        [Required]
        [Column("hwup_description", TypeName = "char(64)")]
        public string Description { get; set; } = "";

        /// <summary>
        /// 文件导入本论坛的时间
        /// </summary>
        [Required]
        [Column("hwup_date_add", TypeName = "datetime")]
        public DateTime DateAdd { get; set; }

        /// <summary>
        /// 文件是否已删除('0':可正常显示/下载 '1':不显示/不提供下载 注意:enum不要当int处理)
        /// </summary>
        [Required]
        [DefaultValue("0")]
        [Column("hwup_is_del", TypeName = "enum('0','1')")]
        public string IsDel { get; set; } = "";

        /// <summary>
        /// 备注
        /// </summary>
        [Column("hwup_comment", TypeName = "text")]
        public string? Comment { get; set; }

        /// <summary>
        /// 导航属性 - 所属学期
        /// </summary>
        [JsonIgnore]
        public Term term { get; set; } = null!;

        /// <summary>
        /// 配置数据库关系
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder实例</param>
        public static void configureDb(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HomeworkUploaded>()
                .HasKey(h => new { h.Term, h.CCode, h.Id });

            modelBuilder.Entity<HomeworkUploaded>()
                .HasOne(h => h.term)
                .WithMany(t => t.homeworks_uploaded)
                .HasForeignKey(h => h.Term)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

}
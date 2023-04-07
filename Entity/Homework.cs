using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Forum.Entity
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// 详细作业信息表
    /// </summary>
    public class Homework
    {
        /// <summary>
        /// 学期(主键+外键)
        /// </summary>
        [Required]
        [Column("hw_term", TypeName = "char(5)")]
        public string Term { get; set; } = "";

        /// <summary>
        /// 课程序号(主键,对应course中的course_code 注意:不是外键关系,但要检查是否存在)
        /// </summary>
        [Required]
        [Column("hw_ccode", TypeName = "char(16)")]
        public string CourseCode { get; set; } = "";

        /// <summary>
        /// 作业序号(主键)
        /// </summary>
        [Required]
        [Column("hw_id", TypeName = "char(4)")]
        public string Id { get; set; } = "";

        /// <summary>
        /// 布置周
        /// </summary>
        [Required]
        [Column("hw_week", TypeName = "tinyint")]
        public byte Week { get; set; }

        /// <summary>
        /// 章节(0-20: 第0-20章 90:大作业 98:文档作业 99:其它作业)
        /// </summary>
        [Required]
        [Column("hw_chapter", TypeName = "tinyint")]
        public byte Chapter { get; set; }

        /// <summary>
        /// 交作业网站的提交文件名
        /// </summary>
        [Required]
        [Column("hw_filename", TypeName = "char(64)")]
        public string FileName { get; set; } = "";

        /// <summary>
        /// 作业描述
        /// </summary>
        [Required]
        [Column("hw_description", TypeName = "char(64)")]
        public string Description { get; set; } = "";

        /// <summary>
        /// 作业提交开始时间
        /// </summary>
        [Required]
        [Column("hw_bdate")]
        public DateTime BeginDate { get; set; }

        /// <summary>
        /// 作业提交结束时间
        /// </summary>
        [Required]
        [Column("hw_edate")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 本作业分数
        /// </summary>
        [Required]
        [Column("hw_score", TypeName = "decimal(5,1)")]
        public decimal Score { get; set; } = 0;

        /// <summary>
        /// 本作业加入论坛的时间
        /// </summary>
        [Required]
        [Column("hw_date_add")]
        public DateTime DateAdded { get; set; }

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
            modelBuilder.Entity<Homework>()
                .HasKey(h => new { h.Term, h.CourseCode, h.Id });

            modelBuilder.Entity<Homework>()
                .HasOne(h => h.term)
                .WithMany(t => t.homeworks)
                .HasForeignKey(h => h.Term)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

}
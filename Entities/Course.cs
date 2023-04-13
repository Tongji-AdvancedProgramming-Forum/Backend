using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Forum.Entities
{
    /*****************************************************************
    课程信息表
        course_term  - 学期(主键+外键)
        course_code  - 课程代码
        course_no    - 课程序号(主键,目前同济的规则是代码+两位序号)
        course_fname - 教务系统中的全名
        course_sname - 课程简称 
        course_type  - 课程类别(1-基础 2-专业，暂时无用，未来和学校的课程编码匹配)
    *****************************************************************/
    /// <summary>
    /// 课程信息表
    /// </summary>
    public class Course
    {

        /// <summary>
        /// 学期(主键+外键)
        /// </summary>
        [Required]
        [Column("course_term", TypeName = "char(5)")]
        public string Term { get; set; } = "";

        /// <summary>
        /// 课程代码
        /// </summary>
        [Required]
        [Column("course_code", TypeName = "char(16)")]
        public string Code { get; set; } = "";

        /// <summary>
        /// 课程序号(主键,目前同济的规则是代码+两位序号)
        /// </summary>
        [Required]
        [Column("course_no", TypeName = "char(16)")]
        public string No { get; set; } = "";

        /// <summary>
        /// 教务系统中的课程全名
        /// </summary>
        [Required]
        [Column("course_fname", TypeName = "char(64)")]
        public string FullName { get; set; } = "";

        /// <summary>
        /// 课程简称
        /// </summary>
        [Required]
        [Column("course_sname", TypeName = "char(32)")]
        public string ShortName { get; set; } = "";

        /// <summary>
        /// 课程类别(1-基础 2-专业，暂时无用，未来和学校的课程编码匹配)
        /// </summary>
        [Required]
        [Column("course_type", TypeName = "char(1)")]
        public string Type { get; set; } = "";

        /// <summary>
        /// 导航属性 - 所属学期
        /// </summary>
        [JsonIgnore]
        public Term term { get; set; } = null!;

        public static void configureDb(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasKey(c => new { c.Code, c.Term });

            modelBuilder.Entity<Course>()
                .HasOne(c => c.term)
                .WithMany(t => t.courses)
                .HasForeignKey(c => c.Term)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Forum.Entities
{
    /*****************************************************************
    学期
        term_no - "2022/2023/2"形式的学期表示
    *****************************************************************/
    /// <summary>
    /// 学期
    /// </summary>
    public class Term
    {
        /// <summary>
        /// "2022/2023/2"形式的学期表示
        /// </summary>
        [Key]
        [Required]
        [Column("term_no", TypeName = "char(5)")]
        public string TermNo { get; set; } = "";

        /// <summary>
        /// 导航属性 - 所拥有学生
        /// </summary>
        public ICollection<Student> students { get; set; } = new List<Student>();

        /// <summary>
        /// 导航属性 - 所拥有课程
        /// </summary>
        public ICollection<Course> courses { get; set; } = new List<Course>();

        /// <summary>
        /// 导航属性 - 所拥有作业
        /// </summary>
        public ICollection<HomeworkUploaded> homeworks_uploaded { get; set; } = new List<HomeworkUploaded>();

        /// <summary>
        /// 导航属性 - 所拥有作业（详细）
        /// </summary>
        public ICollection<Homework> homeworks { get; set; } = new List<Homework>();

        /// <summary>
        /// 配置数据库关系
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder实例</param>
        public static void configureDb(ModelBuilder modelBuilder)
        {

        }
    }
}
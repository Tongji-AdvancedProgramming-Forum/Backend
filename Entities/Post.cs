using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Entities
{
    public enum PostType
    {
        Question,
        QuestionsAdditional,
        Answer,
        Other,
        Default
    }

    /// <summary>
    /// 发帖信息表
    /// </summary>
    public class Post
    {
        /// <summary>
        /// ID(主键,自动增长)
        /// </summary>
        [Column("post_id", TypeName = "int")]
        public int Id { get; set; }

        /// <summary>
        /// 学期(外键)
        /// </summary>
        [Required]
        [Column("post_term", TypeName = "char(5)")]
        public string Term { get; set; } = "";

        /// <summary>
        /// 课程序号(对应course中的course_code 注意:不是外键关系,但要检查是否存在)
        /// </summary>
        [Required]
        [Column("post_ccode", TypeName = "char(16)")]
        public string CourseCode { get; set; } = "";

        /// <summary>
        /// 对应的上传文件/具体作业的序号
        ///  - 如果是在第x周的整体问题处发帖,则本字段值为上传文件序号('22232-000001-W0101' - 必须在homework_uploaded中存在)
        ///  - 如果是在第x周的某具体作业处发帖,则本字段值为具体作业序号('0401' - 必须在homework中存在)
        ///  - 如果是在课程的整体问题处发帖(仅管理员及超级用户允许),则本字段值为"学期-G5位序号-W4位周次"('22232-G00001-W0101'),
        /// </summary>
        [Required]
        [Column("post_hwup_or_hw_id", TypeName = "char(20)")]
        public string HwupOrHwId { get; set; } = "";

        /// <summary>
        /// 布置周(课程的整体问题则周次为-1)
        /// </summary>
        [Required]
        [Column("post_week", TypeName = "tinyint")]
        public byte Week { get; set; }

        /// <summary>
        /// 章节(课程的整体问题则章节为-1)
        /// </summary>
        [Required]
        [Column("post_chapter", TypeName = "tinyint")]
        public byte Chapter { get; set; }

        /// <summary>
        /// 对应帖子的id(与post_id是外键关系)
        /// - 如果是发帖,则为NULL
        /// - 如果是回帖,则为 对应帖子的post_id(以此为依据构建发帖回帖的树形结构)
        /// </summary>
        [Column("post_answer_id", TypeName = "int")]
        public int? AnswerId { get; set; }

        /// <summary>
        /// 帖子类型('Question':首发问题 'QuestionsAdditional':追问 'Answer':回帖 'Other':其它 '/':预留)
        /// - 以 post_term + post_ccode + post_hwup_or_hw_id 为基准汇聚,具体排序规则?
        /// </summary>
        [Required]
        [Column("post_type", TypeName = "enum('Question','QuestionsAdditional','Answer','Other','/')")]
        public PostType Type { get; set; }

        /// <summary>
        /// 发帖人学号
        /// </summary>
        [Required]
        [Column("post_sno", TypeName = "char(8)")]
        public string StuNo { get; set; } = "";

        /// <summary>
        /// 优先级
        /// - (从'0'~'9' 依次递增,帖子显示是按优先级顺序,相同优先级按发帖时间,可由管理员手工置位进行调整)
        /// </summary>
        [Required]
        [Column("post_priority", TypeName = "char(1)")]
        public char Priority { get; set; }

        /// <summary>
        /// 约定的tag标记(0:此标记未置位 1:此标记已置位)
        /// </summary>
        [Required]
        [DefaultValue("0")]
        [Column("post_tag_01", TypeName = "enum('0','1')")]
        public string Tag01 { get; set; } = "0";

        /// <summary>
        /// 约定的tag标记(0:此标记未置位 1:此标记已置位)
        /// </summary>
        [Required]
        [DefaultValue("0")]
        [Column("post_tag_02", TypeName = "enum('0','1')")]
        public string Tag02 { get; set; } = "0";

        /// <summary>
        /// 约定的tag标记(0:此标记未置位 1:此标记已置位)
        /// </summary>
        [Required]
        [DefaultValue("0")]
        [Column("post_tag_03", TypeName = "enum('0','1')")]
        public string Tag03 { get; set; } = "0";

        /// <summary>
        /// 约定的tag标记(0:此标记未置位 1:此标记已置位)
        /// </summary>
        [Required]
        [DefaultValue("0")]
        [Column("post_tag_04", TypeName = "enum('0','1')")]
        public string Tag04 { get; set; } = "0";

        /// <summary>
        /// 约定的tag标记(0:此标记未置位 1:此标记已置位)
        /// </summary>
        [Required]
        [DefaultValue("0")]
        [Column("post_tag_05", TypeName = "enum('0','1')")]
        public string Tag05 { get; set; } = "0";

        /// <summary>
        /// 约定的tag标记(0:此标记未置位 1:此标记已置位)
        /// </summary>
        [Required]
        [DefaultValue("0")]
        [Column("post_tag_06", TypeName = "enum('0','1')")]
        public string Tag06 { get; set; } = "0";

        /// <summary>
        /// 约定的tag标记(0:此标记未置位 1:此标记已置位)
        /// </summary>
        [Required]
        [DefaultValue("0")]
        [Column("post_tag_07", TypeName = "enum('0','1')")]
        public string Tag07 { get; set; } = "0";

        /// <summary>
        /// 约定的tag标记(0:此标记未置位 1:此标记已置位)
        /// </summary>
        [Required]
        [DefaultValue("0")]
        [Column("post_tag_08", TypeName = "enum('0','1')")]
        public string Tag08 { get; set; } = "0";

        /// <summary>
        /// 约定的tag标记(0:此标记未置位 1:此标记已置位)
        /// </summary>
        [Required]
        [DefaultValue("0")]
        [Column("post_tag_09", TypeName = "enum('0','1')")]
        public string Tag09 { get; set; } = "0";

        /// <summary>
        /// 约定的tag标记(0:此标记未置位 1:此标记已置位)
        /// </summary>
        [Required]
        [DefaultValue("0")]
        [Column("post_tag_10", TypeName = "enum('0','1')")]
        public string Tag10 { get; set; } = "0";

        /// <summary>
        /// 发帖具体内容(允许贴图,Richtext?)
        /// </summary>
        [Column("post_content", TypeName = "text")]
        public string? Content { get; set; }

        /// <summary>
        /// 发帖时间
        /// </summary>
        [Required]
        [Column("post_date", TypeName = "datetime")]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// 帖子是否已删除('0':正常显示 '1':不显示,包括所有的回帖 注意:enum不要当int处理)
        /// </summary>
        [Required]
        [DefaultValue("0")]
        [Column("post_is_del", TypeName = "enum('0','1')")]
        public string IsDel { get; set; } = "0";

        /// <summary>
        /// 备注(预留)
        /// </summary>
        [Column("post_comment", TypeName = "text")]
        public string Comment { get; set; } = "";

        /// <summary>
        /// 导航属性 - 附属帖子（回复、追问等）
        /// </summary>
        public ICollection<Post> sub_posts { get; set; } = new List<Post>();

        /// <summary>
        /// 配置数据库关系
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder实例</param>
        public static void ConfigureDb(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Post>()
                .HasOne<Term>()
                .WithMany()
                .HasForeignKey(p => p.Term)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Post>()
                .HasOne<Post>()
                .WithMany(p => p.sub_posts)
                .HasForeignKey(p => p.AnswerId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
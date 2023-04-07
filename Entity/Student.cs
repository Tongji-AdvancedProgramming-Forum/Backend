using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using MySql.Data.MySqlClient;

namespace Forum.Entity
{

    /*****************************************************************
    用户信息表
        stu_term         - 学期(与term表有外键关系)
        stu_grade        - 年级
        stu_no           - 学号(主键)
        stu_name         - 姓名
        stu_password     - 密码(md5)
        stu_sex          - 性别
        stu_class_fname  - 专业/班级全称
        stu_class_sname  - 专业/班级简称
        stu_userlevel    - 学生用户等级(0:普通用户 1:助教 5:管理员 9:超级用户)
        stu_enable       - 账号是否启用('0':禁止登录 '1':允许登录 注意:enum不要当int处理)
        stu_add_date     - 系统注册时间
        stu_cno_1        - 学生选修的课程1的课号(与course表有外键关系)
        stu_cno_1_is_del - 学生选修的课程1是否退课('0':正常 '1':已退课 注意:enum不要当int处理,退课学生对应课程的作业信息不要显示出来)
        stu_cno_2        - 学生选修的课程2的课号(与course表有外键关系)
        stu_cno_2_is_del - 学生选修的课程2是否退课('0':正常 '1':已退课 注意:enum不要当int处理,退课学生对应课程的作业信息不要显示出来)
        stu_cno_3        - 学生选修的课程3的课号(与course表有外键关系)
        stu_cno_3_is_del - 学生选修的课程3是否退课('0':正常 '1':已退课 注意:enum不要当int处理,退课学生对应课程的作业信息不要显示出来)
        stu_is_del       - 该学生是否被删除('0':正常 '1':已删除 注意:被删除则无论stu_enbale置何值均不允许登录)
        stu_comment      - 备注信息
    *****************************************************************/
    public class Student
    {
        /// <summary>
        /// 学期 (与term表有外键关系)
        /// </summary>
        [Required]
        [Column("stu_term", TypeName = "char(5)")]
        public string Term { get; set; } = "";

        /// <summary>
        /// 年级
        /// </summary>
        [Required]
        [Column("stu_grade", TypeName = "char(4)")]
        public string Grade { get; set; } = "";

        /// <summary>
        /// 学号 (主键)
        /// </summary>
        [Key]
        [Required]
        [Column("stu_no", TypeName = "char(8)")]
        public string StuNo { get; set; } = "";

        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        [Column("stu_name", TypeName = "char(32)")]
        public string Name { get; set; } = "";

        /// <summary>
        /// 性别
        /// </summary>
        [Required]
        [DefaultValue("/")]
        [Column("stu_sex", TypeName = "enum('男','女','/')")]
        public string Sex { get; set; } = "";

        /// <summary>
        /// 密码 (md5)
        /// </summary>
        [Required]
        [Column("stu_password", TypeName = "char(32)")]
        public string Password { get; set; } = "";

        /// <summary>
        /// 专业/班级全称
        /// </summary>
        [Required]
        [Column("stu_class_fname", TypeName = "char(32)")]
        public string ClassFullName { get; set; } = "";

        /// <summary>
        /// 专业/班级简称
        /// </summary>
        [Required]
        [Column("stu_class_sname", TypeName = "char(16)")]
        public string ClassShortName { get; set; } = "";

        /// <summary>
        /// 学生用户等级 (0:普通用户 1:助教 5:管理员 9:超级用户)
        /// </summary>
        [Required]
        [Column("stu_userlevel", TypeName = "char(1)")]
        public string UserLevel { get; set; } = "";

        /// <summary>
        /// 账号是否启用 ('0':禁止登录 '1':允许登录)
        /// </summary>
        [Required]
        [DefaultValue("1")]
        [Column("stu_enable", TypeName = "enum('0','1')")]
        public string Enable { get; set; } = "";

        /// <summary>
        /// 系统注册时间
        /// </summary>
        [Required]
        [Column("stu_add_date", TypeName = "datetime")]
        public DateTime AddDate { get; set; }

        /// <summary>
        /// 学生选修的课程1的课号 (与course表有外键关系)
        /// </summary>
        [Required]
        [Column("stu_cno_1", TypeName = "char(16)")]
        public string CourseNo_1 { get; set; } = "";

        /// <summary>
        /// 学生选修的课程1是否退课 ('0':正常 '1':已退课)
        /// </summary>
        [Required]
        [DefaultValue("0")]
        [Column("stu_cno_1_is_del", TypeName = "enum(('0','1')")]
        public string CourseNo_1_IsDel { get; set; } = "";

        /// <summary>
        /// 学生选修的课程2的课号 (与course表有外键关系)
        /// </summary>
        [Required]
        [Column("stu_cno_2", TypeName = "char(16)")]
        public string CourseNo_2 { get; set; } = "";

        /// <summary>
        /// 学生选修的课程2是否退课 ('0':正常 '1':已退课)
        /// </summary>
        [Required]
        [DefaultValue("0")]
        [Column("stu_cno_2_is_del", TypeName = "enum(('0','1')")]
        public string CourseNo_2_IsDel { get; set; } = "";

        /// <summary>
        /// 学生选修的课程3的课号 (与course表有外键关系)
        /// </summary>
        [Required]
        [Column("stu_cno_3", TypeName = "char(16)")]
        public string CourseNo_3 { get; set; } = "";

        /// <summary>
        /// 学生选修的课程3是否退课 ('0':正常 '1':已退课)
        /// </summary>
        [Required]
        [DefaultValue("0")]
        [Column("stu_cno_3_is_del", TypeName = "enum(('0','1')")]
        public string CourseNo_3_IsDel { get; set; } = "";

        /// <summary>
        /// 该学生是否被删除 ('0':正常 '1':已删除)
        /// </summary>
        [Required]
        [DefaultValue("0")]
        [Column("stu_is_del", TypeName = "enum('0','1')")]
        public string IsDel { get; set; } = "";

        /// <summary>
        /// 备注信息
        /// </summary>
        [Required]
        [Column("stu_comment", TypeName = "text")]
        public string Comment { get; set; } = "";

        /// <summary>
        /// 导航属性 - 所属课程
        /// </summary>
        public Course course { get; set; } = null!;

        /// <summary>
        /// 配置数据库关系
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder实例</param>
        public static void configureDb(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasOne<Term>()
                .WithMany(t => t.students)
                .HasForeignKey(s => s.Term)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.course)
                .WithMany()
                .HasForeignKey(s => new { s.CourseNo_1, s.Term })
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
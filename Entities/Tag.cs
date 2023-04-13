using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Entities
{
    /// <summary>
    /// 帖子标记对照表
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// post表中tag的字段名
        /// </summary>
        [Key]
        [Column("tag_fieldname", TypeName = "char(16)")]
        public string FieldName { get; set; } = "";

        /// <summary>
        /// tag的中文解释
        /// </summary>
        [Required]
        [Column("tag_name", TypeName = "char(32)")]
        public string Name { get; set; } = "";

        /// <summary>
        /// 对应tag的前景色(FF0000 - RGB方式表示的颜色,每两位表示一个16进制的颜色)
        /// </summary>
        [Required]
        [Column("tag_fgcolor", TypeName = "char(6)")]
        public string FgColor { get; set; } = "";

        /// <summary>
        /// 对应tag的背景色(00FF00 - RGB方式表示的颜色,每两位表示一个16进制的颜色)
        /// </summary>
        [Required]
        [Column("tag_bgcolor", TypeName = "char(6)")]
        public string BgColor { get; set; } = "";

        /// <summary>
        /// 配置数据库关系
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder实例</param>
        public static void configureDb(ModelBuilder modelBuilder)
        {

        }
    }
}

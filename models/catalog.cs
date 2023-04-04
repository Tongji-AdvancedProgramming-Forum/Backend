using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{

    public class Catalog
    {
        [Key]
        [Required]
        [Column(TypeName = "varchar(20)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = "";

        [Required]
        public int Priority { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Name { get; set; } = "";

        // 节点类型。暂定：0为目录，1为题目，2为文件
        [Required]
        public int Type { get; set; }

        [DefaultValue("Default Name")]
        [Column(TypeName = "varchar(20)")]
        public string Parent_Id { get; set; } = "";

        [ForeignKey("Parent_Id")]
        public Catalog patent_catalog { get; set; } = null!;

        public ICollection<Catalog> child_catalog { get; set; } = new List<Catalog>();

        public ICollection<File> child_file { get; set; } = new List<File>();

        public ICollection<Question> child_question { get; set; } = new List<Question>();
    }
}
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{

    public class Question
    {
        [Key]
        [Required]
        [Column(TypeName = "varchar(20)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = "";

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Title { get; set; } = "";

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Catalog_Id { get; set; } = "";

        [ForeignKey("Catalog_Id")]
        public Catalog catalog { get; set; } = null!;

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Creator_Id { get; set; } = "";

        [ForeignKey("Creator_Id")]
        public User creator { get; set; } = null!;

        [Required]
        [Column(TypeName = "text")]
        public string Content { get; set; } = "";

        [Required]
        [ReadOnly(true)]
        public int Created_At { get; set; }

        [Required]
        public int Updated_At { get; set; }

        // 类型。暂定1=提问,2=公告/助教答疑(不可回复)
        [Required]
        public int Type { get; set; }

        // Bot已经读取
        [Required]
        public int Bot_Read { get; set; }

    }
}
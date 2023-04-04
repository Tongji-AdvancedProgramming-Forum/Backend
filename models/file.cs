using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{

    public class File
    {
        [Key]
        [Required]
        [Column(TypeName = "varchar(20)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = "";

        // 保存文件类型，便于今后拓展功能或设置http响应头
        [Column(TypeName = "varchar(20)")]
        public string Content_Type { get; set; } = "";

        [Required]
        public string Url { get; set; } = "";

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Catalog_Id { get; set; } = "";

        [ForeignKey("Catalog_Id")]
        public Catalog parent_catalog { get; set; } = null!;

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Uploader_Id { get; set; } = "";

        [ForeignKey("Uploader_Id")]
        public User uploader { get; set; } = null!;

    }
}
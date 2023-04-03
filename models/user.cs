using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Forum.Models
{
    public class User
    {
        [Key]
        [Required]
        [Column(TypeName = "varchar(20)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = "";

        [Required]
        [Column(TypeName = "varchar(40)")]
        public string Name { get; set; } = "";

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string Class { get; set; } = "";

        [Required]
        // 暂定Role的规则为：0=学生,1=助教,2=教师
        public int Role { get; set; } = 0;

        // 诸如2152955-04-张尧的名字
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string DisplayName { get; set; } = "";

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string AvatarUrl { get; set; } = "";

        public ICollection<Token> tokens { get; set; } = new List<Token>();

        public ICollection<File> files { get; set; } = new List<File>();

        public ICollection<Question> questions { get; set; } = new List<Question>();
    }
}
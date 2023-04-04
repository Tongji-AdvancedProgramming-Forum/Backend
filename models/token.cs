using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Forum.Models
{
    public class Token
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("User_Id")]
        public User user { get; set; } = null!;

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string User_Id { get; set; } = "";

        [Required]
        [Column(TypeName = "text")]
        public string Content { get; set; } = "";

        [Required]
        [ReadOnly(true)]
        [Column(TypeName = "TIMESTAMP")]
        public DateTime Expired_At { get; set; }
    }
}
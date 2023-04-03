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
        [ReadOnly(true)]
        public int Expired_At { get; set; }
    }
}
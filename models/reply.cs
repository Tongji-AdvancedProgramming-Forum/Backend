using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{

    public class Reply
    {
        [Key]
        [Required]
        [Column(TypeName = "varchar(20)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = "";

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Question_Id { get; set; } = "";

        [ForeignKey("Question_Id")]
        public Question question { get; set; } = null!;

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Creator_Id { get; set; } = "";

        [ForeignKey("Creator_Id")]
        public User creator { get; set; } = null!;

        [Required]
        [Column(TypeName = "text")]
        public string Content { get; set; } = "";

        [Required]
        [Column(TypeName = "TIMESTAMP")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created_At { get; set; }

        [Required]
        [Column(TypeName = "TIMESTAMP")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Updated_At { get; set; }

    }
}
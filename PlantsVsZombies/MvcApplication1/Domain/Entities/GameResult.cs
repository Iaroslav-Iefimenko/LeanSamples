using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class GameResult
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public Int32 UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public Int32 DestroyedZombies { get; set; }

        [Required]
        public DateTime GameDate { get; set; }
    }
}
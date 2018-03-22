using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Photo
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
        public byte[] Image { get; set; }

        [StringLength(2048)]
        public String Comment { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}

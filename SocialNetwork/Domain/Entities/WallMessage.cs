using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class WallMessage
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }

        [Required]
        [ForeignKey("WallOwnerUser")]
        public Int32 WallOwnerId { get; set; }

        public virtual User WallOwnerUser { get; set; }

        [Required]
        [ForeignKey("AuthorUser")]
        public Int32 AuthorId { get; set; }

        public virtual User AuthorUser { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public String Text { get; set; }
    }
}

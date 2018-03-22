using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Message
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }

        [Required]
        [ForeignKey("UserTo")]
        public Int32 UserToId { get; set; }

        public virtual User UserTo { get; set; }

        [Required]
        [ForeignKey("UserFrom")]
        public Int32 UserFromId { get; set; }

        public virtual User UserFrom { get; set; }

        [Required]
        [StringLength(1024)]
        public String Text { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}

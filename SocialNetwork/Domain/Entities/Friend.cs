using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Friend
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
        [ForeignKey("FriendUser")]
        public Int32 FriendId { get; set; }

        public virtual User FriendUser { get; set; }
    }
}

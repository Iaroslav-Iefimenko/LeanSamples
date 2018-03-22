using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class FriendRequest
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
        [ForeignKey("PossibleFriendUser")]
        public Int32 PossibleFriendId { get; set; }

        public virtual User PossibleFriendUser { get; set; }
    }
}

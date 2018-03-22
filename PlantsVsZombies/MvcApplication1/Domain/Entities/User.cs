using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class User
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }

        [Required]
        [StringLength(128)]
        public String UserName { get; set; }

        [Required]
        [StringLength(128)]
        public String Password { get; set; }

        [Required]
        [StringLength(128)]
        [RegularExpression(@"^[a-zA-Z0-9.-]{1,20}@[a-zA-Z0-9]{1,20}\.[A-Za-z]{2,4}")]
        public String Email { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(128)]
        public String FirstName { get; set; }

        [Required]
        [StringLength(128)]
        public String LastName { get; set; }

        [Required]
        [StringLength(128)]
        public String MiddleName { get; set; }

        [ForeignKey("UserId")]
        public virtual ICollection<GameResult> GameResults { get; set; }
    }
}

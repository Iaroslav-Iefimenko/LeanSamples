using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class GameSetting
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }

        [Required]
        [StringLength(100)]
        public String Name { get; set; }

        [Required]
        [StringLength(1024)]
        public String Value { get; set; }
    }
}
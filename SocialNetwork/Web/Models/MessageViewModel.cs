using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class MessageViewModel
    {
        //входящее сообщение
        public Int32 Id { get; set; }
        public Int32 UserId { get; set; }
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(1024)]
        public String Text { get; set; }
    }
}
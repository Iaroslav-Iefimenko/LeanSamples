using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class WallMessageViewModel
    {
        public Int32 Id { get; set; }
        public Int32 WallOwnerId { get; set; }
        public Int32 AuthorId { get; set; }
        public String AuthorName { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required(ErrorMessage = "*")]
        public String Text { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Web.Models
{
    [Bind(Exclude = "Image")]
    public class PhotoViewModel
    {
        public Int32 Id { get; set; }
        public Int32 UserId { get; set; }
        [ScaffoldColumn(false)]
        public byte[] Image { get; set; }
        public String Comment { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
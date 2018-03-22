using System;
using System.Collections.Generic;

namespace Web.Models
{
    public class PhotosViewModel
    {
        public List<PhotoViewModel> Photos { get; set; }
        public Int32 UserId { get; set; }
        public String UserName { get; set; }
        public Boolean UserIsMe { get; set; }
    }
}
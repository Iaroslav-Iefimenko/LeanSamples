using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "*")]
        public String UserName { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public String Password { get; set; }
    }
}
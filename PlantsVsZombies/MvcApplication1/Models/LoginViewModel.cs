using System;
using System.ComponentModel.DataAnnotations;

namespace Models
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
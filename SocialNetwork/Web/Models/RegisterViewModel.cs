using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "*")]
        [StringLength(128, ErrorMessage = "Логин не может быть больше 128 символов.")]
        public String UserName { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [StringLength(128, ErrorMessage = "Пароль не может быть больше 128 символов.")]
        public String Password { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [StringLength(128, ErrorMessage = "Пароль не может быть больше 128 символов.")]
        [Compare("Password", ErrorMessage = "Пароль подтвержден неверно")]
        public String ConfirmPassword { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(128, ErrorMessage = "E-mail не может быть больше 128 символов.")]
        [RegularExpression(@"^[a-zA-Z0-9.-]{1,20}@[a-zA-Z0-9]{1,20}\.[A-Za-z]{2,4}",
            ErrorMessage = "Неверный формат Email")]
        public String Email { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(128, ErrorMessage = "E-mail не может быть больше 128 символов.")]
        [RegularExpression(@"^[a-zA-Z0-9.-]{1,20}@[a-zA-Z0-9]{1,20}\.[A-Za-z]{2,4}",
            ErrorMessage = "Неверный формат Email")]
        [Compare("Email", ErrorMessage = "E-mail подтвержден неверно")]
        public String ConfirmEmail { get; set; }
        
        [Required(ErrorMessage = "*")]
        [StringLength(128, ErrorMessage = "Имя не может быть больше 128 символов.")]
        public String FirstName { get; set; }
        
        [Required(ErrorMessage = "*")]
        [StringLength(128, ErrorMessage = "Фамилия не может быть больше 128 символов.")]
        public String LastName { get; set; }
        
        [Required(ErrorMessage = "*")]
        [StringLength(128, ErrorMessage = "Отечество не может быть больше 128 символов.")]
        public String MiddleName { get; set; }
    }
}
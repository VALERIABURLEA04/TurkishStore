using System;
using System.ComponentModel.DataAnnotations;

namespace businessLogic.Dtos.UserDtos
{
    public class UserLoginDto
    {
        [Required]
        [Display(Name = "Email")]
        public string NameOrEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public DateTime LoginDataTime { get; set; }
    }
}
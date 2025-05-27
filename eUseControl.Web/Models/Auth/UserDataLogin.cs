using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace lab_1_templates_sandu.Models.Auth
{
	public class UserDataLogin
	{
        [Required]
        [Display(Name = "Username or Email")]
        public string NameOrEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
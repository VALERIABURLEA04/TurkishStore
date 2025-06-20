using eUseControl.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace businessLogic.Dtos.UserDtos
{
    public class UpsertUserDto
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Username")]
        [StringLength(30, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Registration Date")]
        public DateTime RegisterDataTime { get; set; }

        public UserRole Level { get; set; }
    }
}
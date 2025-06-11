using eUseControl.Domain.Entities.ProductEntities;
using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eUseControl.Domain.Entities.UserEntities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Username")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Username must be between 6 characters and 50 charachers.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "Password must be between 6 characters and 50 charachers.")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastLogin { get; set; }

        [StringLength(30)]
        public string UserIp { get; set; }

        public UserRole Level { get; set; }

        [InverseProperty("User")]
        public ICollection<ProductToUser> ProductsToUsers { get; set; } = new HashSet<ProductToUser>();
    }
}
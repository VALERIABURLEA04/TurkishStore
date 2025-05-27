using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eUseControl.Domain.Entities.Admin
{
    [Table("AdminData")]
    public class AdminData
    {
        [Key]
        public int Id { get; set; }  // Primary key

        [Required]
        [MaxLength(50)] // example max length, adjust to your DB schema
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace eUseControl.Domain.Entities.User
{
    [Table("TBLUserInfo")]
    public class UserInfo
    {
        [Key]
        public int IdUs { get; set; }

        [Column("UsernameUS")]
        public string UsernameUS { get; set; }

        [Column("PasswordUs")]
        public string PasswordUs { get; set; }

        [Column("Role")]
        public string Role { get; set; }

    }

}
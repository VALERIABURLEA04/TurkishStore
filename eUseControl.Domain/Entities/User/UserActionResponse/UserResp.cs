using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Enums;

namespace eUseControl.Domain.Entities.User.UserActionResponse
{
    public class UserResp
    {
        public int UserId { get; set; }
        public bool Status { get; set; }
        public LogInResult Result { get; set; }
        public UserRole Role { get; set; }
        public string Name { get; set; }
    }
}

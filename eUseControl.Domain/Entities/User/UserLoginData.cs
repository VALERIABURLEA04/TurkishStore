using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.User
{
    public class UserLoginData
    {
        public string NameOrEmail { get; set; }
        public string Password { get; set; }
        public string UserIp { get; set; }
        public DateTime LoginDataTime { get; set; }
    }
}

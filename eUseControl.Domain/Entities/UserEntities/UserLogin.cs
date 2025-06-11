using System;

namespace eUseControl.Domain.Entities.UserEntities
{
    public class UserLogin
    {
        public string NameOrEmail { get; set; }
        public string Password { get; set; }
        public string UserIp { get; set; }
        public DateTime LoginDataTime { get; set; }
    }
}
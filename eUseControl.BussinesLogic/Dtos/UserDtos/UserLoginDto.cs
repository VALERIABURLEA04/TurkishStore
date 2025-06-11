using System;

namespace businessLogic.Dtos.UserDtos
{
    public class UserLoginDto
    {
        public string NameOrEmail { get; set; }
        public string Password { get; set; }
        public DateTime LoginDataTime { get; set; }
    }
}
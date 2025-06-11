using System;

namespace eUseControl.Domain.Entities.UserEntities
{
    public class UserRegister
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime RegisterDataTime { get; set; }
    }
}
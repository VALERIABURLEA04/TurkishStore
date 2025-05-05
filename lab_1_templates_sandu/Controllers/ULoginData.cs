using System;

namespace ProjectOnlineStore.Controllers
{
    internal class ULoginData
    {
        public object Credential { get; set; }
        public object Password { get; set; }
        public string LoginIp { get; set; }
        public DateTime LoginDateTime { get; set; }
    }
}
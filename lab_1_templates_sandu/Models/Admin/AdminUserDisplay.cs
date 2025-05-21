using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab_1_templates_sandu.Models.Admin
{
	public class AdminUserDisplay
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime LastLogin { get; set; }
        public string UserIp { get; set; }
        public string Role { get; set; }
    }
}
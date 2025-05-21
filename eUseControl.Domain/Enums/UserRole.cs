using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Enums
{
    public enum UserRole
    {
        [Description("Guest User")]
        Guest = 0,

        [Description("Regular User")]
        User = 100,

        [Description("Moderator User")]
        Moderator = 200,

        [Description("Site Admin")]
        Admin = 300
    }
}

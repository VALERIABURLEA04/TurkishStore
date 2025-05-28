using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using businessLogic.Interfaces;
using eUseControl.Domain.Entities.Admin;
using eUseControlBussinessLogic.Core;

namespace businessLogic.BLStruct
{
    public class AdminBL : AdminApi, IAdmin
    {
        public AdminData AdminLogin(string username, string password)
        {
            return ValidateAdminLogin(username, password);
        }
    }
}
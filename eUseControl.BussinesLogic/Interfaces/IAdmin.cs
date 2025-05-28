using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Entities.Admin;

namespace businessLogic.Interfaces
{
    public interface IAdmin
    {
        AdminData AdminLogin(string username, string password);
    }
}
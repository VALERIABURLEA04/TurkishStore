using BusinessLogic.DBModel;
using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControlBussinessLogic.Core
{
    public class UserApi
    {


        public bool RegisterUser(UserInfo user)
        {
            using (var db = new DataContext())
            {
                db.TBLUserInfo.Add(user);
                db.SaveChanges();
                return true;
            }
        }

        //public (bool isValid, string role) ValidateUser(UserInfo userInfo)
        //{
        //    throw new NotImplementedException();
    }
}
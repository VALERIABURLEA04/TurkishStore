﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Entities.Admin;


namespace businessLogic.Interfaces
{
    public interface IUser
    {
        Task<UserTable> GetUserFromCookieAsync(HttpRequestBase request);
        bool IsUserAdmin(UserTable user);
        bool IsOwnerOrAdmin(UserTable user, int ownerId);

        Task<UserTable> GetUserByUsernameOrEmailAsync(string identifier);

        List<AdminUserDisplay> GetAllUsers();
        bool DeleteUser(int id);
    }
}
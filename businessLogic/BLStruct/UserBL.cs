using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using businessLogic.Interfaces;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Enums;
using System.Web;
using businessLogic.DBModel;
using eUseControl.Helpers.Session;
using System.Data.Entity;
using eUseControl.Domain.Entities.Admin;

namespace businessLogic.BLStruct
{
    public class UserBL : IUser
    {
        public async Task<UserTable> GetUserFromCookieAsync(HttpRequestBase request)
        {
            if (request.Cookies["X-KEY"] == null)
                return null;

            var xKey = request.Cookies["X-KEY"].Value;
            var usernameOrEmail = CookieGenerator.Validate(xKey);

            if (string.IsNullOrEmpty(usernameOrEmail))
                return null;

            return await Task.Run(() =>
            {
                using (var db = new UserContext())
                {
                    return db.Users.FirstOrDefault(u => u.Name == usernameOrEmail || u.Email == usernameOrEmail);
                }
            });
        }

        public bool IsUserAdmin(UserTable user)
        {
            if (user == null)
                return false;

            return user.Level == UserRole.Admin;
        }

        public bool IsOwnerOrAdmin(UserTable user, int ownerId)
        {
            if (user == null)
                return false;

            return user.Id == ownerId || IsUserAdmin(user);
        }

        public async Task<UserTable> GetUserByUsernameOrEmailAsync(string identifier)
        {
            using (var db = new UserContext())
            {
                return await db.Users
                    .FirstOrDefaultAsync(u => u.Name == identifier || u.Email == identifier);
            }
        }

        public List<AdminUserDisplay> GetAllUsers()
        {
            using (var userDb = new UserContext())
            {
                return userDb.Users.Select(u => new AdminUserDisplay
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    LastLogin = u.LastLogin,
                    UserIp = u.UserIp,
                    Role = u.Level.ToString()
                }).ToList();
            }
        }

        public bool DeleteUser(int id)
        {
            using (var userDb = new UserContext())
            {
                var user = userDb.Users.Find(id);
                if (user == null)
                    return false;

                userDb.Users.Remove(user);
                userDb.SaveChanges();
                return true;
            }
        }
    }
}
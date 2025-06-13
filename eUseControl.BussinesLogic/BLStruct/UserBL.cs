using businessLogic.Interfaces;
using BusinessLogic.DBModel;
using eUseControl.Domain.Entities.UserEntities;
using eUseControl.Domain.Enums;
using eUseControl.Helpers.Session;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace businessLogic.BLStruct
{
    public class UserBL : IUser
    {
        public async Task<User> GetUserFromCookieAsync(HttpRequestBase request)
        {
            if (request.Cookies["X-KEY"] == null)
                return null;

            var xKey = request.Cookies["X-KEY"].Value;
            var usernameOrEmail = CookieGenerator.Validate(xKey);

            if (string.IsNullOrEmpty(usernameOrEmail))
                return null;

            return await Task.Run(() =>
            {
                using (var db = new EUseControlDbContext())
                {
                    return db.Users.FirstOrDefault(u => u.Name == usernameOrEmail || u.Email == usernameOrEmail);
                }
            });
        }

        public bool IsUserAdmin(User user)
        {
            if (user == null)
                return false;

            return user.Level == UserRole.Admin;
        }

        public bool IsOwnerOrAdmin(User user, int ownerId)
        {
            if (user == null)
                return false;

            return user.Id == ownerId || IsUserAdmin(user);
        }

        public async Task<User> GetUserByUsernameOrEmailAsync(string identifier)
        {
            using (var db = new EUseControlDbContext())
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
using businessLogic.DBModel;
using businessLogic.Interfaces;
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
                using (var db = new UserContext())
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
            using (var db = new UserContext())
            {
                return await db.Users
                    .FirstOrDefaultAsync(u => u.Name == identifier || u.Email == identifier);
            }
        }
    }
}
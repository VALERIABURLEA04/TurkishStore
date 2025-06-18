using eUseControl.Domain.Entities.UserEntities;
using eUseControl.Domain.Enums;
using eUSeControl.BusinessLogic.Interfaces;
using eUSeControl.Common.Session;
using eUSeControl.DataAccess.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace eUSeControl.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private static UserService _instance;
        private static readonly object _lock = new object();

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

        public static UserService GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new UserService();
                }
            }

            return _instance;
        }
    }
}
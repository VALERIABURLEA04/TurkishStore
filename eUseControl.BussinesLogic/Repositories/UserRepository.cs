using eUseControl.Domain.Entities.UserEntities;
using eUseControl.Domain.Repositories;
using eUSeControl.DataAccess.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;

namespace businessLogic.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EUseControlDbContext _context;

        private static UserRepository _instance;
        private static readonly object _lock = new object();

        private UserRepository()
        {
            _context = EUseControlDbContext.GetInstance();
        }

        public static UserRepository GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new UserRepository();
                }
            }
            return _instance;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public void DeleteUserById(int userId)
        {
            if (userId == 0)
                return;

            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            _context.Users.Remove(user);

            _context.SaveChanges();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            if (id == 0)
                return new User();

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task<bool> ExistsByUsernameOrEmailAsync(string username, string email)
        {
            return await _context.Users.AnyAsync(u => u.Name == username || u.Email == email);
        }

        public async Task<bool> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            await Task.CompletedTask;
            return true;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _context.Users.AddOrUpdate(user);
            _context.SaveChanges();

            await Task.CompletedTask;
            return true;
        }
    }
}
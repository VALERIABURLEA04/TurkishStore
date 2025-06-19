using eUseControl.Domain.Entities.UserEntities;
using eUseControl.Domain.Repositories;
using eUSeControl.DataAccess.Data;
using System.Collections.Generic;
using System.Data.Entity;
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
    }
}
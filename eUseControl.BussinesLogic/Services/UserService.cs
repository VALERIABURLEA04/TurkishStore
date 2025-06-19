using businessLogic.Dtos.UserDtos;
using businessLogic.Repositories;
using eUseControl.Domain.Entities.UserEntities;
using eUseControl.Domain.Enums;
using eUseControl.Domain.Repositories;
using eUSeControl.BusinessLogic.Interfaces;
using eUSeControl.Common.Session;
using eUSeControl.DataAccess.Data;
using System;
using System.Collections.Generic;
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

        private readonly IUserRepository _userRepository;

        public UserService()
        {
            _userRepository = UserRepository.GetInstance();
        }

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

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();

            var result = users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Role = Enum.GetName(typeof(UserRole), u.Level),
                    LastLogin = u.LastLogin.ToString("dd/MM/yyyy")
                })
                .ToList();

            return result;
        }

        public void DeleteUserById(int id)
        {
            _userRepository.DeleteUserById(id);
        }
    }
}
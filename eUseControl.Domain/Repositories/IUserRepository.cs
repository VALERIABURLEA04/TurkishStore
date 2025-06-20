using eUseControl.Domain.Entities.UserEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eUseControl.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();

        void DeleteUserById(int userId);

        Task<User> GetUserByIdAsync(int id);

        Task<bool> ExistsByUsernameOrEmailAsync(string username, string email);

        Task<bool> AddUserAsync(User user);

        Task<bool> UpdateUserAsync(User user);
    }
}
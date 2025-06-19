using eUseControl.Domain.Entities.UserEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eUseControl.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();

        void DeleteUserById(int userId);
    }
}
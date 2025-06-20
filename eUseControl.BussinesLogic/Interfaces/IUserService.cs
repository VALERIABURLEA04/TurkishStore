using businessLogic.Dtos.UserDtos;
using eUseControl.Domain.Entities.UserEntities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace eUSeControl.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserFromCookieAsync(HttpRequestBase request);

        bool IsUserAdmin(User user);

        bool IsOwnerOrAdmin(User user, int ownerId);

        Task<User> GetUserByUsernameOrEmailAsync(string identifier);

        Task<List<UserDto>> GetAllUsersAsync();

        void DeleteUserById(int id);

        Task<UpsertUserDto> GetUserByIdAsync(int id);

        Task<bool> AddUserAsync(UpsertUserDto model);

        Task<bool> UpdateUserAsync(UpsertUserDto model);
    }
}
using eUseControl.Domain.Entities.UserEntities;
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
    }
}
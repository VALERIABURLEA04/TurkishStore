using System.Collections.Generic;
using System.Linq;
using eUseControl.Domain.Entities.Admin;
using eUseControl.Web.Models.Admin;

namespace eUseControl.Web.Logic.Mappers
{
    public static class UserDataMapper
    {
        // Domain → View Model
        public static AdminUserDisplayModel ToViewModel(AdminUserDisplay user) => new AdminUserDisplayModel
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            LastLogin = user.LastLogin,
            UserIp = user.UserIp,
            Role = user.Role
        };

        // View Model → Domain
        public static AdminUserDisplay ToDomain(AdminUserDisplayModel model) => new AdminUserDisplay
        {
            Id = model.Id,
            Name = model.Name,
            Email = model.Email,
            LastLogin = model.LastLogin,
            UserIp = model.UserIp,
            Role = model.Role
        };

        // List conversions
        public static List<AdminUserDisplayModel> ToViewModelList(IEnumerable<AdminUserDisplay> users) =>
            users.Select(ToViewModel).ToList();

        public static List<AdminUserDisplay> ToDomainList(IEnumerable<AdminUserDisplayModel> models) =>
            models.Select(ToDomain).ToList();
    }
}

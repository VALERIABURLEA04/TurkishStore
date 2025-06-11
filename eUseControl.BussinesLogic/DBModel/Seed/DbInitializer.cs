using businessLogic.DBModel;
using eUseControl.Domain.Entities.UserEntities;
using eUseControl.Helpers.AccessFlow;
using System;
using System.Linq;

public class DbInitializer
{
    public static void SeedAdmin()
    {
        using (var context = new UserContext())
        {
            if (!context.Users.Any(u => u.Email == "admin@example.com"))
            {
                var adminUser = new User
                {
                    Name = "administrator",
                    Email = "admin@example.com",
                    Password = AccessHelper.HashPassword("admin123!"),
                    Level = eUseControl.Domain.Enums.UserRole.Admin,
                    LastLogin = DateTime.Now,
                    UserIp = "127.0.0.1"
                };

                context.Users.Add(adminUser);
                context.SaveChanges();
            }
        }
    }
}
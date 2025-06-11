using businessLogic.DBModel;
using businessLogic.Dtos.UserDtos;
using eUseControl.Domain.Entities.SessionEntities;
using eUseControl.Domain.Entities.UserEntities;
using eUseControl.Domain.Enums;
using eUseControl.Helpers.AccessFlow;
using eUseControl.Helpers.Session;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace eUseControlBussinessLogic.Core
{
    public class UserApi
    {
        public string RegisterUser(UserRegisterDto model)
        {
            using (var db = new UserContext())
            {
                if (db.Users.Any(u => u.Name == model.Name || u.Email == model.Email))
                    return null;

                var user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = AccessHelper.HashPassword(model.Password),
                    LastLogin = model.RegisterDataTime,
                    UserIp = HttpContext.Current?.Request.UserHostAddress,
                    Level = UserRole.User
                };

                db.Users.Add(user);
                db.SaveChanges();

                string token = AccessHelper.GenerateSecureToken(user.Id);

                return token;
            }
        }

        public UserRespDto LogInUser(UserLoginDto model)
        {
            try
            {
                string hashedPassword = AccessHelper.HashPassword(model.Password);
                User user;

                using (var dbContext = new UserContext())
                {
                    user = dbContext.Users.FirstOrDefault(u => u.Email == model.NameOrEmail);

                    if (user == null)
                    {
                        return new UserRespDto
                        {
                            Status = false,
                            Result = LogInResult.EmailNotFound
                        };
                    }

                    if (user.Password != hashedPassword)
                    {
                        return new UserRespDto
                        {
                            Status = false,
                            Result = LogInResult.WrongPassword
                        };
                    }
                }

                user.LastLogin = model.LoginDataTime;
                user.UserIp = HttpContext.Current?.Request.UserHostAddress;

                using (var db = new UserContext())
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return new UserRespDto
                {
                    Status = true,
                    Result = LogInResult.Success,
                    UserId = user.Id,
                    Role = user.Level,
                    Name = user.Name
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UserLogInLogic: {ex.Message}");
                return new UserRespDto
                {
                    Status = false,
                    Result = LogInResult.UnknownError,
                };
            }
        }

        public UserCookieRespDto GenerateCookieByUserAction(int userId)
        {
            var cookieString = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(userId + HttpContext.Current?.Request.UserHostAddress)
            };

            Session sessionDb;

            using (var db = new SessionContext())
            {
                sessionDb = db.Sessions.FirstOrDefault(u => u.UserId == userId);
            }

            var dateTime = DateTime.Now;

            if (sessionDb != null)
            {
                sessionDb.Cookie = cookieString.Value;
                sessionDb.ValidTime = dateTime.AddHours(3);

                using (var db = new SessionContext())
                {
                    db.Entry(sessionDb).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else
            {
                sessionDb = new Session()
                {
                    UserId = userId,
                    Cookie = cookieString.Value,
                    ValidTime = dateTime.AddHours(3)
                };

                using (var db = new SessionContext())
                {
                    db.Sessions.Add(sessionDb);
                    db.SaveChanges();
                }
            }

            return new UserCookieRespDto()
            {
                UserId = userId,
                Cookie = cookieString,
                ExpirationDate = dateTime
            };
        }

        public UserRespDto GetUserByCookieAction(string cookieKey)
        {
            Session session;
            User user;

            using (var db = new SessionContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.Cookie.Contains(cookieKey));
            }

            if (session != null)
            {
                using (var db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Id == session.UserId);
                }

                if (user != null)
                {
                    return new UserRespDto()
                    {
                        UserId = user.Id,
                        Status = true,
                        Role = user.Level,
                        Name = user.Name
                    };
                }
            }

            return new UserRespDto()
            {
                Status = false,
            };
        }
    }
}
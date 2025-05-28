using businessLogic.DBModel;
using BusinessLogic.DBModel;
using eUseControl.Domain.Entities.Session;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Entities.User.UserActionResponse;
using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using eUseControl.Helpers.AccessFlow;
using eUseControl.Helpers.Session;

namespace eUseControlBussinessLogic.Core
{
    public class UserApi
    {
        public string RegisterUser(UserRegisterData model)
        {
            using (var db = new UserContext())
            {
                if (db.Users.Any(u => u.Name == model.Name || u.Email == model.Email))
                    return null;

                var user = new UserTable
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

        public UserResp LogInUser(UserLoginData model)
        {
            try
            {
                string hashedPassword = AccessHelper.HashPassword(model.Password);
                UserTable user;

                using (var dbContext = new UserContext())
                {
                    user = dbContext.Users.FirstOrDefault(u => u.Email == model.NameOrEmail || u.Name == model.NameOrEmail);

                    if (user == null)
                    {
                        return new UserResp
                        {
                            Status = false,
                            Result = LogInResult.EmailNotFound
                        };
                    }

                    if (user.Password != hashedPassword)
                    {
                        return new UserResp
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

                return new UserResp
                {
                    Status = true,
                    Result = LogInResult.Success,
                    UserId = user.Id,
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UserLogInLogic: {ex.Message}");
                return new UserResp
                {
                    Status = false,
                    Result = LogInResult.UnknownError,
                };
            }
        }

        public UserCookieResp GenerateCookieByUserAction(int userId)
        {
            var cookieString = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(userId + HttpContext.Current?.Request.UserHostAddress)
            };


            SessionTable sessionDb;

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
                sessionDb = new SessionTable()
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

            return new UserCookieResp()
            {
                UserId = userId,
                Cookie = cookieString,
                ExpirationDate = dateTime
            };
        }

        public UserResp GetUserByCookieAction(string cookieKey)
        {
            SessionTable session;
            UserTable user;

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
                    return new UserResp()
                    {
                        UserId = user.Id,
                        Status = true,
                        Role = user.Level,
                        Name = user.Name
                    };
                }
            }

            return new UserResp()
            {
                Status = false,
            };
        }
    }

}
using businessLogic.Interfaces;
using System;

namespace businessLogic
{
    class SessionManager : ISessionManager
    {
        // Implement StartSession method
        public void StartSession(string userId)
        {
            // Your logic for starting the session
            Console.WriteLine($"Session started for user: {userId}");
        }

        // Implement EndSession method
        public void EndSession()
        {
            // Your logic for ending the session
            Console.WriteLine("Session ended.");
        }

        // Implement UserLogin method
        UserLoginResult UserLogin(ULoginData data)
        {
            // Example login check (replace with actual logic)
            if (data.Credential == "admin" && data.Password == "password")
            {
                return new UserLoginResult
                {
                    Status = true,
                    StatusMsg = "Login successful"
                };
            }
            else
            {
                return new UserLoginResult
                {
                    Status = false,
                    StatusMsg = "Invalid login"
                };
            }
        }

        UserLoginResult ISessionManager.UserLogin(ULoginData data)
        {
            return UserLogin(data);
        }
    }
}

using System;
using eUseControlBussinessLogic.Core;
using eUseControlBussinessLogic.Interfaces;

namespace eUseControlBussinessLogic
{
    public class SessionBL : UserApi, ISession
    {
        // Implement StartSession method
        public void StartSession(string userId)
        {
            // Logic to start a session for the user
            Console.WriteLine($"Session started for user: {userId}");
        }

        // Implement EndSession method
        public void EndSession()
        {
            // Logic to end the session
            Console.WriteLine("Session ended.");
        }
    }
}

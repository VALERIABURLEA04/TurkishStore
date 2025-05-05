namespace businessLogic.Interfaces
{
    interface ISessionManager
    {
        void StartSession(string userId);  // Method signature for starting a session
        void EndSession();  // Method signature for ending a session

        UserLoginResult UserLogin(ULoginData data);  // Method signature for user login
    }
}

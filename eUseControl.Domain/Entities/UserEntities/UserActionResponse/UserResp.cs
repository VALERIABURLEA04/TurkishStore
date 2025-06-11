using eUseControl.Domain.Enums;

namespace eUseControl.Domain.Entities.UserEntities.UserActionResponse
{
    public class UserResp
    {
        public int UserId { get; set; }
        public bool Status { get; set; }
        public LogInResult Result { get; set; }
        public UserRole Role { get; set; }
        public string Name { get; set; }
    }
}
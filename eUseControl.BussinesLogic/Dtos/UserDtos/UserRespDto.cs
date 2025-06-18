using eUseControl.Domain.Enums;

namespace eUSeControl.BusinessLogic.Dtos.UserDtos
{
    public class UserRespDto
    {
        public bool Status { get; set; }
        public LogInResult Result { get; set; }
        public int UserId { get; set; }
        public UserRole Role { get; set; }
        public string Name { get; set; }
    }
}
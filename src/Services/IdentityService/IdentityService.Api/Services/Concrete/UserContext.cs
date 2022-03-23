using System;

namespace IdentityService.Api.Services.Concrete
{
    public class UserContext
    {
        public string DialingCode { get; set; }
        public string MobilePhone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ProfileImageName { get; set; }
        public DateTime? ExpireDate { get; set; }
        public Boolean? IsActivation { get; set; }
        public Boolean? IsFirstLogin { get; set; }
        public Boolean IsMailActivation { get; set; }
        public DateTime? LastPasswordChangeDate { get; set; }
    }
}

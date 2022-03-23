using System;

namespace IdentityService.Api.Resources
{
    public class OTPResponseModel
    {
        public string DialingCode { get; set; }
        public string MobilePhone { get; set; }
        public string OTPCode { get; set; }
        public DateTime ValidDate { get; set; }
    }
}

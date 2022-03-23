using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IdentityService.Api.Resources
{
    public class UserResource
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required(ErrorMessage = "Dialing Code is required")]
        public string DialingCode { get; set; }

        [Required(ErrorMessage = "Mobile Phone is required")]
        public string MobilePhone { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ProfileImageName { get; set; }
        public DateTime? ExpireDate { get; set; }
        public Boolean IsActivation { get; set; }
        public Boolean? IsFirstLogin { get; set; }
        public Boolean? IsMailActivation { get; set; }
        public DateTime? LastPasswordChangeDate { get; set; }
    }
}

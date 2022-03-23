using AutoMapper;
using IdentityService.Api.Models;
using IdentityService.Api.Resources;

namespace IdentityService.Api.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<UserResource, User>().ReverseMap();
        }
    }
}

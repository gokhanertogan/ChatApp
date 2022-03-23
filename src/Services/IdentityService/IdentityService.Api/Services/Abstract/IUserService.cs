using ChatApp.Shared.Dtos;
using IdentityService.Api.Resources;
using System.Threading.Tasks;

namespace IdentityService.Api.Services.Abstract
{
    public interface IUserService
    {
        Task<Response<UserResource>> AddAsync(UserResource productResource);
        Response<NoContent> Update(UserResource productResouce, int productId);
        Task<Response<NoContent>> DeleteAsync(int productId);
        Task<Response<UserResource>> GetByPhoneNumberAsync(string dialingCode, string mobilePhone);
        Response<OTPResponseModel> SendOTP(string dialingCode, string mobilePhone);
    }
}

using AutoMapper;
using ChatApp.Shared.Dtos;
using IdentityService.Api.Helpers;
using IdentityService.Api.Infrastructure.Repositories;
using IdentityService.Api.Infrastructure.UnitOfWork;
using IdentityService.Api.Models;
using IdentityService.Api.Resources;
using IdentityService.Api.Services.Abstract;
using IdentityService.Api.Settings;
using System;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Api.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGeneralConfigs _generalConfigs;
        private readonly ISmsHelper _smsHelper;

        public UserService(IMapper mapper, IUserRepository userRepository, IUnitOfWork unitOfWork, IGeneralConfigs generalConfigs, ISmsHelper smsHelper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _generalConfigs = generalConfigs;
            _smsHelper = smsHelper;
        }

        public async Task<Response<UserResource>> AddAsync(UserResource userResource)
        {
            try
            {
                var user = _mapper.Map<UserResource, User>(userResource);
                await _userRepository.AddAsync(user);
                await _unitOfWork.CompleteAsync();

                userResource.Id = user.Id;
                return Response<UserResource>.Success(userResource, 200);
            }

            catch (Exception ex)
            {
                return Response<UserResource>.Fail($"Error an occurred while adding a product : {ex.Message}", 500);
            }
        }

        public Response<NoContent> Update(UserResource userResouce, int userId)
        {
            var user = _mapper.Map<User>(userResouce);
            _userRepository.Update(user, userId);
            _unitOfWork.Complete();

            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> DeleteAsync(int userId)
        {
            try
            {
                await _userRepository.DeleteAsync(userId);
                await _unitOfWork.CompleteAsync();

                return Response<NoContent>.Success(204);
            }

            catch (Exception ex)
            {
                return Response<NoContent>.Fail($"Error an occurred while deleting the product : {ex.Message}", 500);
            }
        }

        public async Task<Response<UserResource>> GetByPhoneNumberAsync(string dialingCode, string mobilePhone)
        {
            try
            {
                var user = await _userRepository.GetByPhoneNumberAsync(dialingCode, mobilePhone);

                if (user != null)
                {
                    return Response<UserResource>.Success(_mapper.Map<UserResource>(user), 200);
                }

                return Response<UserResource>.Success(null, 404);
            }

            catch (Exception ex)
            {
                return Response<UserResource>.Fail($"Error an occurred : {ex.Message}", 500);
            }
        }

        public Response<OTPResponseModel> SendOTP(string dialingCode, string mobilePhone)
        {
            try
            {
                OTPResponseModel responseModel = new OTPResponseModel();
                responseModel.DialingCode = dialingCode;
                responseModel.MobilePhone = mobilePhone;
                responseModel.ValidDate = DateTime.Now.AddMinutes(5);
                responseModel.OTPCode = GenerateOtpCode(4);

                _smsHelper.Send(new SmsModel
                {
                    Text = $"bla bla bla your otp code : {responseModel.OTPCode}",
                    To = responseModel.DialingCode + responseModel.MobilePhone
                });

                return Response<OTPResponseModel>.Success(responseModel, 200);
            }
            catch (Exception ex)
            {
                return Response<OTPResponseModel>.Fail($"Error an occurred while sending a otp code : {ex.Message}", 500);
            }
        }

        private string GenerateOtpCode(int lengthOfPassword)
        {
            if (_generalConfigs.IsRandomOtpCode)
            {
                string valid = "1234567890";
                StringBuilder strB = new StringBuilder(100);
                Random random = new Random();
                while (0 < lengthOfPassword--)
                {
                    strB.Append(valid[random.Next(valid.Length)]);
                }
                return strB.ToString();
            }

            return "1111";
        }

    }
}

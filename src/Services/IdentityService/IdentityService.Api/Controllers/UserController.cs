using IdentityService.Api.Resources;
using IdentityService.Api.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ChatApp.Shared.ControllerBases;
using IdentityService.Api.Extensions;

namespace IdentityService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserResource userResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            else
            {
                var response = await _userService.AddAsync(userResource);
                return CreateActionResultInstance(response);
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _userService.DeleteAsync(id);

            return CreateActionResultInstance(response);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(UserResource userResource, int id)
        {
            var response = _userService.Update(userResource, id);

            return CreateActionResultInstance(response);
        }

        [HttpGet]
        [Route("/api/[controller]/[action]/GetByPhoneNumber")]
        public async Task<IActionResult> GetByPhoneNumberAsync(string dialingCode, string mobilePhone)
        {
            var response = await _userService.GetByPhoneNumberAsync(dialingCode, mobilePhone);

            return CreateActionResultInstance(response);
        }

        [HttpGet]
        [Route("/api/[controller]/SendOTPMessage")]
        public IActionResult SendOTP(string dialingCode, string mobilePhone)
        {
            var response = _userService.SendOTP(dialingCode, mobilePhone);

            return CreateActionResultInstance(response);
        }
    }
}

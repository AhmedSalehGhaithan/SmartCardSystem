using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartCardSystem.DTOs;
using SmartCardSystem.Repository;
using static SmartCardSystem.Responses.CustomResponses;
namespace SmartCardSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountController(IAccount _account) : ControllerBase
    {
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<RegistrationResponse>> RegisterAsync(RegisterDTO model)
        {
            var result = await _account.RegisterAsync(model);
            return Ok(result);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> LoginAsync(LoginDTO model)
        {
            var result = await _account.LoginAsync(model);
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public ActionResult<LoginResponse> RefreshToken(UserSession model)
        {
            var result = _account.RefreshToken(model);
            return Ok(result);
        }


    }
}

using Authentication.Helpers;
using Authentication.Models;
using Authentication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJWTManagerService _jwtManangerService;
        private readonly IUserService _userService;

        public AuthController(IJWTManagerService jwtManagerService, IUserService userService)
        {
            _jwtManangerService = jwtManagerService;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(TokenModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> Authenticate(CredentialModel credential)
        {
            var token = await _jwtManangerService.AuthAsync(credential);

            if (token == null)
                return Unauthorized();

            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("NewUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> NewUser(CredentialModel credential)
        {
            await _userService.NewUser(credential);
            return Ok();
        }
    }
}

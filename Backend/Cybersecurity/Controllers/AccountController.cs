using Cybersecurity.Authentication;
using Cybersecurity.Interfaces.Services;
using Cybersecurity.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cybersecurity.Controllers
{
    [Route("api/account")]
    [ApiController]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto registerDto)
        {
            await _accountService.RegisterUser(registerDto);

            return Ok("Ok");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserDto loginDto)
        {
            var userRole = await _accountService.LoginUser(loginDto);

            return Ok(userRole);
        }

        [HttpPost("logout")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout();
            
            return Ok("succes");
        }

        [HttpPut("update/{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UpdateUserDto updateDto)
        {
            await _accountService.UpdateUser(id, updateDto);

            return Ok();
        }

        [HttpPut("password/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePassword([FromRoute] int id, [FromBody] ChangePasswordDto changePasswordDto)
        {
            await _accountService.ChangePassword(id, changePasswordDto);

            Response.Cookies.Delete("changePassword");

            return Ok("succes");
        }

        [HttpGet("user")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _accountService.GetAllUsers();

            return Ok(users);
        }

        [HttpGet("user/{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            var user = await _accountService.GetUser(id);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _accountService.GetRoles();

            return Ok(roles);
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            await _accountService.DeleteUser(id);

            return Ok("succes");
        }
    }
}

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
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogService _logService;

        public AccountController(IAccountService accountService, IAuthenticationService authenticationService, ILogService logService)
        {
            _accountService = accountService;
            _authenticationService = authenticationService;
            _logService = logService;
        }

        [HttpPost("register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto registerDto)
        {
            var jwt = Request.Cookies["jwt"];

            string userId = "0";

            if (jwt != null)
            {
                userId = await _authenticationService.GetIdFromClaim(jwt);  
            } 

            await _accountService.RegisterUser(registerDto, userId);

            return Ok("Ok");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserDto loginDto)
        {
            var user = await _accountService.LoginUser(loginDto);

            if(user.IsPasswordExpire)
            {
                Response.Cookies.Append("changePassword", user.Id.ToString(), new CookieOptions { });
                return BadRequest("Należy zmienić hało");
            }

            var token = await _authenticationService.Generate(user.Id, user.RoleName);

            Response.Cookies.Append("jwt", token, new CookieOptions { });
            Response.Cookies.Append("login", "true", new CookieOptions { });

            return Ok("succes");
        }

        [HttpPost("logout")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Logout()
        {
            var jwt = Request.Cookies["jwt"];

            var userId = _authenticationService.GetIdFromClaim(jwt);

            await _logService.AddLog("Wylogowanie użytkownika", "Wylgowanie", Convert.ToInt32(userId));

            Response.Cookies.Delete("jwt");
            Response.Cookies.Delete("login");

            await Task.CompletedTask;
            
            return Ok("succes");
        }

        [HttpPut("update/{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UpdateUserDto updateDto)
        {
            await _accountService.UpdateUser(id, updateDto);

            var jwt = Request.Cookies["jwt"];

            var userId = _authenticationService.GetIdFromClaim(jwt);

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
        
        [HttpGet("test")]
        public void Test()
        {
            var jwt = Request.Cookies["jwt"];

            Console.WriteLine(_authenticationService.GetIdFromClaim(jwt).Result);
        }
    }
}

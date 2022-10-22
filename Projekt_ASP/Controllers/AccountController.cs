using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projekt_ASP.Data;
using Projekt_ASP.DTO;
using Projekt_ASP.Interfaces;
using System.Net;

namespace Projekt_ASP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;

        }


       
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAll()
        {

            var collection = await _userService.GetAllUser();
            return Ok(collection);
        }
        

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Login user)
        {

            try
            {
                return Ok(await _userService.LoginAsync(user.login, user.password));
            }
            catch (Exception)
            {

                return Ok(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> Post([FromBody] ChangePassword user)
        {
            try
            {
                await _userService.ChangePassword(user);
                return Ok(HttpStatusCode.OK);
            }
            catch (Exception )
            {
                
                return Ok(HttpStatusCode.BadRequest);
            }
            

            
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post(User user)
        {
            await _userService.RegisterAsync(user.Login, user.Password, user.Role);
            return Ok();
        }

        [HttpPost("tokenAuth")]
        public async Task<IActionResult> Post([FromBody] TokenAuthDto token)
        {
            try
            {
                return Ok(await _userService.GetAccountByToken(token.Token));
            }
            catch (Exception)
            {

                return Ok(HttpStatusCode.BadRequest);
            }
            
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> Post([FromBody] CreateUserDto user)
        {
            try
            {
                await _userService.GetCreateUser(user);
                return Ok(HttpStatusCode.OK);
            }
            catch (Exception)
            {

                return Ok(HttpStatusCode.BadRequest);
            }

        }

    }
}

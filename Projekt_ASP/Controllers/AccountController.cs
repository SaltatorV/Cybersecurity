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


       
        [HttpGet("GetUsers")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var collection = await _userService.GetAllUser();
                return Ok(collection);
            }
            catch (Exception)
            {

                return Ok(HttpStatusCode.BadRequest);
            }
            
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
            catch (Exception e)
            {
                
                return Ok(e.Message);
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
        [HttpDelete("deleteUser/{login}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string login)
        {
            try
            {
                await _userService.DeleteUserLoginAsync(login);
                return Ok(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return Ok(HttpStatusCode.BadRequest);

            }
        }

        [HttpPut("zablokuj/{login}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(string login)
        {
            try
            {
                await _userService.ZablokujUserAsync(login);
                return Ok(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return Ok(HttpStatusCode.BadRequest);

            }
        }

        [HttpPut("odblokuj/{login}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put2(string login)
        {
            try
            {
                await _userService.OdblokujUserAsync(login);
                return Ok(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return Ok(HttpStatusCode.BadRequest);

            }
        }

        [HttpPost("OpcjeHasel")]
        [Authorize (Roles ="Admin")]
        public async Task<IActionResult> Post([FromBody] OpcjeHaselDto opcje)
        {
            try
            {
                await _userService.OpcjeHasel(opcje);
                return Ok(HttpStatusCode.OK);
            }
            catch (Exception)
            {

                return Ok(HttpStatusCode.BadRequest);
            }

        }

        [HttpGet("CzyWygaslo/{login}")]
        public async Task<IActionResult> Get(string login)
        {
            try
            {

                return Ok(await _userService.CzyWygasloService(login));
            }
            catch (Exception)
            {

                return Ok(HttpStatusCode.BadRequest);
            }

        }


    }
}

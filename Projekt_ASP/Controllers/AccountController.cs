﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        public async Task<IActionResult> Post(Login user)
        {
            return Ok(await _userService.LoginAsync(user.login, user.password));
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> Post(ChangePassword user)
        {
            await _userService.ChangePassword(user);
            return Ok(HttpStatusCode.OK);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post(User user)
        {
            await _userService.RegisterAsync(user.Login, user.Password, user.Role);
            return Ok();
        }

    }
}

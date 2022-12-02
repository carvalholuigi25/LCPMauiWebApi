﻿using Microsoft.AspNetCore.Mvc;
using LCPMauiWebApi.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace LCPMauiWebApi.Server.Controllers
{
    [Route("api/app/users")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public AppUserController(UserManager<ApplicationUser> userManager, ILogger logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public async Task<ApplicationUser> GetListAuthAppUser(string uname = "carvalholuigi26@gmail.com")
        {
            _logger.LogInformation("Fetching the list of auth of app users...");
            var user = await _userManager.FindByNameAsync(uname);
            return user!;
        }

        [Authorize]
        [HttpGet("info")]
        public async Task<IActionResult> GetAppUsers(string email = "carvalholuigi25@gmail.com")
        {
            var myuser = await _userManager.FindByEmailAsync(email);
            return Ok(new {
                IsAuthorized = User.Identity?.IsAuthenticated,
                Id = myuser?.Id,
                Email = myuser?.Email,
                Username = myuser?.UserName,
                PhoneNumber = myuser?.PhoneNumber
            });
        }

        [Authorize]
        [HttpGet("atoken")]
        public async Task<IActionResult> GetAccessToken()
        {
            var result = await HttpContext.GetTokenAsync("access_token") ?? HttpContext.Request.Headers["Authorization"];
            return Ok(result);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
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
            var myclaims = await _userManager.GetClaimsAsync(myuser!);
            var myroles = await _userManager.GetRolesAsync(myuser!);
            var res = new
            {
                Id = myuser?.Id,
                Email = myuser?.Email,
                Username = myuser?.UserName,
                PhoneNumber = myuser?.PhoneNumber,
                EmailConfirmed = myuser?.EmailConfirmed,
                PhoneNumberConfirmed = myuser?.PhoneNumberConfirmed,
                AccessFailedCount = myuser?.AccessFailedCount,
                ConcurrencyStamp = myuser?.ConcurrencyStamp,
                LockoutEnabled = myuser?.LockoutEnabled,
                LockoutEnd = myuser?.LockoutEnd,
                SecurityStamp = myuser?.SecurityStamp,
                TwoFactorEnabled = myuser?.TwoFactorEnabled,
                IsAuthorized = User.Identity?.IsAuthenticated,
                Tokens = new
                {
                    IdToken = await HttpContext.GetTokenAsync("id_token") ?? Guid.NewGuid().ToString(),
                    AccessToken = await HttpContext.GetTokenAsync("access_token"),
                    RefreshToken = await HttpContext.GetTokenAsync("refresh_token"),
                    ExpiresAtToken = await HttpContext.GetTokenAsync("expires_at")
                },
                Claims = myclaims,
                Roles = myroles
            };

            if (res != null)
            {
                await System.IO.File.WriteAllTextAsync(Directory.GetCurrentDirectory() + @"\Data\session.json", Newtonsoft.Json.JsonConvert.SerializeObject(res, Newtonsoft.Json.Formatting.Indented));
            }

            return Ok(res);
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

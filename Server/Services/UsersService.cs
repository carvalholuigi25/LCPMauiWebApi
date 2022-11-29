using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BC = BCrypt.Net.BCrypt;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using LCPMauiWebApi.Server.Classes;
using LCPMauiWebApi.Server.Interfaces;
using LCPMauiWebApi.Server.Context;

namespace LCPMauiWebApi.Server.Services
{
    //for the best pratices of web api controllers (and others, etc...), heres the link: https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-7.0&tabs=visual-studio

    public class UsersService : ControllerBase, IUsersService
    {
        private readonly DBContext _dbc;
        private readonly IConfiguration _iconfig;
        private readonly int salt = 11;

        public UsersService(DBContext dbc, IConfiguration iconfig)
        {
            _dbc = dbc;
            _iconfig = iconfig;
        }

        public async Task<ActionResult<List<MyUsers>>> GetUsers()
        {
            var items = await _dbc.MyUsers.ToListAsync();

            if (items == null)
            {
                return NotFound();
            }

            return items;
        }

        public async Task<ActionResult<List<MyUsersAuth>>> GetUsersAuth()
        {
            var items = await _dbc.MyUsersAuth.ToListAsync();

            if (items == null)
            {
                return NotFound();
            }

            return items;
        }

        public async Task<ActionResult<MyUsers>> GetUsersById(int id)
        {
            var userItem = await _dbc.MyUsers.FindAsync(id);

            if (userItem == null)
            {
                return NotFound();
            }

            return userItem;
            //return await _dbc.Users.Where(x => x.UsersId == id).FirstOrDefaultAsync();
        }

        public async Task<ActionResult<MyUsers>> CreateUsers(MyUsers users)
        {
            if (!string.IsNullOrEmpty(users.Password))
            {
                users.Password = BC.HashPassword(users.Password, salt);
            }

            if (!string.IsNullOrEmpty(users.Pin))
            {
                users.Pin = BC.HashPassword(users.Pin, salt);
            }

            _dbc.MyUsers.Add(users);
            await _dbc.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsers), users);
        }

        public async Task<IActionResult> UpdateUsersById(int id, MyUsers users)
        {
            if (id != users.MyUsersId)
            {
                return BadRequest();
            }

            if (!string.IsNullOrEmpty(users.Password))
            {
                users.Password = BC.HashPassword(users.Password, salt);
            }

            if (!string.IsNullOrEmpty(users.Pin))
            {
                users.Pin = BC.HashPassword(users.Pin, salt);
            }

            _dbc.Entry(users).State = EntityState.Modified;

            try
            {
                await _dbc.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        public async Task<IActionResult> DeleteUsersById(int id)
        {
            var userItem = await _dbc.MyUsers.FindAsync(id);
            if (userItem == null)
            {
                return NotFound();
            }

            _dbc.MyUsers.Remove(userItem);
            await _dbc.SaveChangesAsync();

            return NoContent();
        }

        public async Task<ActionResult> Register(MyUsers users)
        {
            var account = await _dbc.MyUsers.FirstOrDefaultAsync(x => x.Username == users.Username || x.Email == users.Email);

            if (account == null)
            {
                if (!string.IsNullOrEmpty(users.Password))
                {
                    users.Password = BC.HashPassword(users.Password, salt);
                }

                if (!string.IsNullOrEmpty(users.Pin))
                {
                    users.Pin = BC.HashPassword(users.Pin, salt);
                }

                _dbc.MyUsers.Add(users);
                await _dbc.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUsers), users);
            }
            else
            {
                return Ok("Error: The account already has been registered!");
            }
        }

        public async Task<ActionResult> RegisterByAuth(MyUsersAuth usersath)
        {
            var account = await _dbc.MyUsersAuth.FirstOrDefaultAsync(x => x.Username == usersath.Username || x.Email == usersath.Email);

            if (account == null)
            {
                if (!string.IsNullOrEmpty(usersath.Password))
                {
                    usersath.Password = BC.HashPassword(usersath.Password, salt);
                }

                if (!string.IsNullOrEmpty(usersath.Pin))
                {
                    usersath.Pin = BC.HashPassword(usersath.Pin, salt);
                }

                _dbc.MyUsersAuth.Add(usersath);
                await _dbc.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUsersAuth), usersath);
            }
            else
            {
                return Ok("Error: The account already has been registered!");
            }
        }

        public async Task<ActionResult> Authenticate(MyUsersAuth usersath)
        {
            var account = await _dbc.MyUsers.FirstOrDefaultAsync(x => x.Username == usersath.Username);

            if (!string.IsNullOrEmpty(usersath.Email))
            {
                account = await _dbc.MyUsers.FirstOrDefaultAsync(x => x.Email == usersath.Email);
            }

            if (account != null && BC.Verify(usersath.Password, account.Password))
            {
                string token = GenerateToken(usersath);
                var val = new MyUsersAuth()
                {
                    MyUsersAuthId = account.MyUsersId,
                    Username = usersath.Username,
                    Password = usersath.Password,
                    Email = account.Email,
                    Pin = account.Pin,
                    DateCreated = DateTime.Now,
                    DateExpLogin = DateTime.Now.AddDays(7),
                    Token = token
                };

                var valwoid = new MyUsersAuth()
                {
                    Username = usersath.Username,
                    Password = usersath.Password,
                    Email = account.Email,
                    Pin = account.Pin,
                    DateCreated = DateTime.Now,
                    DateExpLogin = DateTime.Now.AddDays(7),
                    Token = token
                };

                await RegisterByAuth(valwoid);
                return Ok(val);
            }
            else
            {
                return Ok("Error: Login failed");
            }
        }

        public bool UsersExists(int id)
        {
            return _dbc.MyUsers.Any(e => e.MyUsersId == id);
        }

        // source: https://www.c-sharpcorner.com/article/asp-net-core-api-clean-architecture-with-jwt-authentication-and-swagger/
        private string GenerateToken(MyUsersAuth usersath)
        {
#nullable disable
            var myuname = !string.IsNullOrEmpty(usersath.Username) ? usersath.Username : usersath.Email;

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, myuname),
                new Claim(ClaimTypes.Role, _dbc.MyUsers.FirstOrDefault(x => x.Username == usersath.Username).Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iconfig.GetSection("token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            DateTime d1 = DateTime.Now;
            DateTime d2 = d1.AddDays(7);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: d2,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

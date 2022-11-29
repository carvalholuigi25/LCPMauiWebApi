using LCPMauiWebApi.Server.Classes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCPMauiWebApi.Server.Interfaces
{
    public interface IUsersService
    {
        public Task<ActionResult<List<MyUsers>>> GetUsers();
        public Task<ActionResult<List<MyUsersAuth>>> GetUsersAuth();
        public Task<ActionResult<MyUsers>> GetUsersById(int id);
        public Task<ActionResult<MyUsers>> CreateUsers(MyUsers users);
        public Task<IActionResult> UpdateUsersById(int id, MyUsers users);
        public Task<IActionResult> DeleteUsersById(int id);
        public Task<ActionResult> Register(MyUsers users);
        public Task<ActionResult> RegisterByAuth(MyUsersAuth usersath);
        public Task<ActionResult> Authenticate(MyUsersAuth usersath);
        public bool UsersExists(int id);
    }
}

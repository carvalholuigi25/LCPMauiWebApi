using Microsoft.AspNetCore.Mvc;
using LCPMauiWebApi.Server.Classes;
using LCPMauiWebApi.Server.Interfaces;

namespace LCPMauiWebApi.Server.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _iuserssrv;
        private readonly ILogger _logger;

        public UsersController(IUsersService iuserssrv, ILogger logger)
        {
            _iuserssrv = iuserssrv;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<MyUsers>>> GetUsers()
        {
            _logger.LogInformation("Get all users executing...");
            return await _iuserssrv.GetUsers();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MyUsers>> GetUsers(int id)
        {
            _logger.LogInformation("Get users by id executing...");
            return await _iuserssrv.GetUsersById(id);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MyUsers>> PostUsers(MyUsers users)
        {
            _logger.LogInformation("Create users executing...");
            return await _iuserssrv.CreateUsers(users);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, MyUsers users)
        {
            _logger.LogInformation("Update users by id executing...");
            return await _iuserssrv.UpdateUsersById(id, users);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            _logger.LogInformation("Delete users by id executing...");
            return await _iuserssrv.DeleteUsersById(id);
        }

        //for authentication purposes
        [HttpGet("list/auth")]
        public async Task<ActionResult<List<MyUsersAuth>>> GetUsersAuth()
        {
            _logger.LogInformation("Get all users from authentication executing...");
            return await _iuserssrv.GetUsersAuth();
        }

        [HttpPost("login")]
        public async Task<ActionResult<MyUsersAuth>> DoLogin(MyUsersAuth usersath)
        {
            _logger.LogInformation("Login users executing...");
            return await _iuserssrv.Authenticate(usersath);
        }

        [HttpPost("register")]
        public async Task<ActionResult<MyUsers>> DoRegister(MyUsers users)
        {
            _logger.LogInformation("Register users executing...");
            return await _iuserssrv.Register(users);
        }

        [HttpPost("auth/special/register")]
        public async Task<ActionResult<MyUsersAuth>> DoRegisterInAuth(MyUsersAuth usersath)
        {
            _logger.LogInformation("Register users in special auth executing...");
            return await _iuserssrv.RegisterByAuth(usersath);
        }
    }
}

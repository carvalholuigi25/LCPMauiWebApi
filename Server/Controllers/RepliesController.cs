using Microsoft.AspNetCore.Mvc;
using LCPMauiWebApi.Server.Interfaces;
using LCPMauiWebApi.Server.Classes;

namespace LCPMauiWebApi.Server.Controllers
{
    [Route("api/replies")]
    [ApiController]
    public class RepliesController : ControllerBase
    {
        private readonly IRepliesService _irepliessrv;
        private readonly ILogger _logger;

        public RepliesController(IRepliesService irepliessrv, ILogger logger)
        {
            _irepliessrv = irepliessrv;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Replies>>> GetReplies()
        {
            _logger.LogInformation("Get all replies executing...");
            return await _irepliessrv.GetReplies();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Replies>> GetReplies(int id)
        {
            _logger.LogInformation("Get replies by id executing...");
            return await _irepliessrv.GetRepliesById(id);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReplies(int id, Replies replies)
        {
            _logger.LogInformation("Update replies by id executing...");
            return await _irepliessrv.UpdateRepliesById(id, replies);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Replies>> PostReplies(Replies replies)
        {
            _logger.LogInformation("Create replies executing...");
            return await _irepliessrv.CreateReplies(replies);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReplies(int id)
        {
            _logger.LogInformation("Delete replies by id executing...");
            return await _irepliessrv.DeleteRepliesById(id);
        }
    }
}

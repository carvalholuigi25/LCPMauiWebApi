using Microsoft.AspNetCore.Mvc;
using LCPMauiWebApi.Server.Classes;
using LCPMauiWebApi.Server.Interfaces;

namespace LCPMauiWebApi.Server.Controllers
{
    [Route("api/reactions")]
    [ApiController]
    public class ReactionsController : ControllerBase
    {
        private readonly IReactionsService _ireactionssrv;
        private readonly ILogger _logger;

        public ReactionsController(IReactionsService ireactionssrv, ILogger logger)
        {
            _ireactionssrv = ireactionssrv;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Reactions>>> GetReactions()
        {
            _logger.LogInformation("Get all reactions executing...");
            return await _ireactionssrv.GetReactions();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reactions>> GetReactions(int id)
        {
            _logger.LogInformation("Get reactions by id executing...");
            return await _ireactionssrv.GetReactionsById(id);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReactions(int id, Reactions reactions)
        {
            _logger.LogInformation("Update reactions by id executing...");
            return await _ireactionssrv.UpdateReactionsById(id, reactions);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reactions>> PostReactions(Reactions reactions)
        {
            _logger.LogInformation("Create reactions executing...");
            return await _ireactionssrv.CreateReactions(reactions);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReactions(int id)
        {
            _logger.LogInformation("Delete reactions by id executing...");
            return await _ireactionssrv.DeleteReactionsById(id);
        }
    }
}

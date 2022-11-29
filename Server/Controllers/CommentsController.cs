using Microsoft.AspNetCore.Mvc;
using LCPMauiWebApi.Server.Classes;
using LCPMauiWebApi.Server.Interfaces;

namespace LCPMauiWebApi.Server.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService _icommentssrv;
        private readonly ILogger _logger;

        public CommentsController(ICommentsService icommentssrv, ILogger logger)
        {
            _icommentssrv = icommentssrv;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Comments>>> GetComments()
        {
            _logger.LogInformation("Get all comments executing...");
            return await _icommentssrv.GetComments();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comments>> GetComments(int id)
        {
            _logger.LogInformation("Get comments by id executing...");
            return await _icommentssrv.GetCommentsById(id);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComments(int id, Comments comments)
        {
            _logger.LogInformation("Update comments by id executing...");
            return await _icommentssrv.UpdateCommentsById(id, comments);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comments>> PostComments(Comments comments)
        {
            _logger.LogInformation("Create comments executing...");
            return await _icommentssrv.CreateComments(comments);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComments(int id)
        {
            _logger.LogInformation("Delete comments executing...");
            return await _icommentssrv.DeleteCommentsById(id);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using LCPMauiWebApi.Server.Classes;
using LCPMauiWebApi.Server.Interfaces;

namespace LCPMauiWebApi.Server.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _ipostssrv;
        private readonly ILogger _logger;

        public PostsController(IPostsService ipostssrv, ILogger logger)
        {
            _ipostssrv = ipostssrv;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Posts>>> GetPosts()
        {
            _logger.LogInformation("Get all posts executing...");
            return await _ipostssrv.GetPosts();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Posts>> GetPosts(int id)
        {
            _logger.LogInformation("Get posts by id executing...");
            return await _ipostssrv.GetPostsById(id);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosts(int id, Posts posts)
        {
            _logger.LogInformation("Update posts by id executing...");
            return await _ipostssrv.UpdatePostsById(id, posts);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Posts>> PostPosts(Posts posts)
        {
            _logger.LogInformation("Create posts executing...");
            return await _ipostssrv.CreatePosts(posts);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosts(int id)
        {
            _logger.LogInformation("Delete posts by id executing...");
            return await _ipostssrv.DeletePostsById(id);
        }
    }
}

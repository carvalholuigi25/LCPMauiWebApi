using Microsoft.AspNetCore.Mvc;
using LCPMauiWebApi.Server.Classes;
using LCPMauiWebApi.Server.Interfaces;

namespace LCPMauiWebApi.Server.Controllers
{
    [Route("api/todo")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _iTodosrv;
        private readonly ILogger _logger;

        public TodoController(ITodoService iTodosrv, ILogger logger)
        {
            _iTodosrv = iTodosrv;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Todo>>> GetTodo()
        {
            _logger.LogInformation("Get all todo items executing...");
            return await _iTodosrv.GetTodo();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(int id)
        {
            _logger.LogInformation("Get todo items by id executing...");
            return await _iTodosrv.GetTodoById(id);
        }

        // To protect from overTodoing attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodo(int id, Todo Todo)
        {
            _logger.LogInformation("Update todo items by id executing...");
            return await _iTodosrv.UpdateTodoById(id, Todo);
        }

        // To protect from overTodoing attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Todo>> TodoTodo(Todo Todo)
        {
            _logger.LogInformation("Create todo items executing...");
            return await _iTodosrv.CreateTodo(Todo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            _logger.LogInformation("Delete todo items by id executing...");
            return await _iTodosrv.DeleteTodoById(id);
        }
    }
}

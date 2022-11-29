using LCPMauiWebApi.Server.Classes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCPMauiWebApi.Server.Interfaces
{
    public interface ITodoService
    {
        public Task<ActionResult<List<Todo>>> GetTodo();
        public Task<ActionResult<Todo>> GetTodoById(int id);
        public Task<ActionResult<Todo>> CreateTodo(Todo Todo);
        public Task<IActionResult> UpdateTodoById(int id, Todo Todo);
        public Task<IActionResult> DeleteTodoById(int id);
        public bool TodoExists(int id);
    }
}

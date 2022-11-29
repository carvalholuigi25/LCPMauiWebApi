using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LCPMauiWebApi.Server.Classes;
using LCPMauiWebApi.Server.Context;
using LCPMauiWebApi.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LCPMauiWebApi.Server.Services
{
    //for the best pratices of web api controllers (and others, etc...), heres the link: https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-7.0&tabs=visual-studio

    public class TodoService : ControllerBase, ITodoService
    {
        private readonly DBContext _dbc;

        public TodoService(DBContext dbc)
        {
            _dbc = dbc;
        }

        public async Task<ActionResult<List<Todo>>> GetTodo()
        {
            var items = await _dbc.Todo.ToListAsync();

            if (items == null)
            {
                return NotFound();
            }

            return items;
        }

        public async Task<ActionResult<Todo>> GetTodoById(int id)
        {
            var TodoItem = await _dbc.Todo.FindAsync(id);

            if (TodoItem == null)
            {
                return NotFound();
            }

            return TodoItem;
            //return await _dbc.Todo.Where(x => x.TodoId == id).FirstOrDefaultAsync();
        }

        public async Task<ActionResult<Todo>> CreateTodo(Todo Todo)
        {
            _dbc.Todo.Add(Todo);
            await _dbc.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodo), Todo);
        }

        public async Task<IActionResult> UpdateTodoById(int id, Todo Todo)
        {
            if (id != Todo.TodoId)
            {
                return BadRequest();
            }

            _dbc.Entry(Todo).State = EntityState.Modified;

            try
            {
                await _dbc.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
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

        public async Task<IActionResult> DeleteTodoById(int id)
        {
            var TodoItem = await _dbc.Todo.FindAsync(id);
            if (TodoItem == null)
            {
                return NotFound();
            }

            _dbc.Todo.Remove(TodoItem);
            await _dbc.SaveChangesAsync();

            return NoContent();
        }

        public bool TodoExists(int id)
        {
            return _dbc.Todo.Any(e => e.TodoId == id);
        }
    }
}

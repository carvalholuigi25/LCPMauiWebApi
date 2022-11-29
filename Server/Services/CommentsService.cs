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

    public class CommentsService : ControllerBase, ICommentsService
    {
        private readonly DBContext _dbc;

        public CommentsService(DBContext dbc)
        {
            _dbc = dbc;
        }

        public async Task<ActionResult<List<Comments>>> GetComments()
        {
            var items = await _dbc.Comments.ToListAsync();

            if (items == null)
            {
                return NotFound();
            }

            return items;
        }

        public async Task<ActionResult<Comments>> GetCommentsById(int id)
        {
            var commentItem = await _dbc.Comments.FindAsync(id);

            if (commentItem == null)
            {
                return NotFound();
            }

            return commentItem;
            //return await _dbc.Comments.Where(x => x.CommentsId == id).FirstOrDefaultAsync();
        }

        public async Task<ActionResult<Comments>> CreateComments(Comments comments)
        {
            _dbc.Comments.Add(comments);
            await _dbc.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComments), comments);
        }

        public async Task<IActionResult> UpdateCommentsById(int id, Comments comments)
        {
            if (id != comments.CommentsId)
            {
                return BadRequest();
            }

            _dbc.Entry(comments).State = EntityState.Modified;

            try
            {
                await _dbc.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentsExists(id))
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

        public async Task<IActionResult> DeleteCommentsById(int id)
        {
            var commentItem = await _dbc.Comments.FindAsync(id);
            if (commentItem == null)
            {
                return NotFound();
            }

            _dbc.Comments.Remove(commentItem);
            await _dbc.SaveChangesAsync();

            return NoContent();
        }

        public bool CommentsExists(int id)
        {
            return _dbc.Comments.Any(e => e.CommentsId == id);
        }
    }
}

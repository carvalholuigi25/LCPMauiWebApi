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

    public class RepliesService : ControllerBase, IRepliesService
    {
        private readonly DBContext _dbc;

        public RepliesService(DBContext dbc)
        {
            _dbc = dbc;
        }

        public async Task<ActionResult<List<Replies>>> GetReplies()
        {
            var items = await _dbc.Replies.ToListAsync();

            if (items == null)
            {
                return NotFound();
            }

            return items;
        }

        public async Task<ActionResult<Replies>> GetRepliesById(int id)
        {
            var replyItem = await _dbc.Replies.FindAsync(id);

            if (replyItem == null)
            {
                return NotFound();
            }

            return replyItem;
            //return await _dbc.Replies.Where(x => x.RepliesId == id).FirstOrDefaultAsync();
        }

        public async Task<ActionResult<Replies>> CreateReplies(Replies replies)
        {
            _dbc.Replies.Add(replies);
            await _dbc.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReplies), replies);
        }

        public async Task<IActionResult> UpdateRepliesById(int id, Replies replies)
        {
            if (id != replies.RepliesId)
            {
                return BadRequest();
            }

            _dbc.Entry(replies).State = EntityState.Modified;

            try
            {
                await _dbc.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RepliesExists(id))
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

        public async Task<IActionResult> DeleteRepliesById(int id)
        {
            var replyItem = await _dbc.Replies.FindAsync(id);
            if (replyItem == null)
            {
                return NotFound();
            }

            _dbc.Replies.Remove(replyItem);
            await _dbc.SaveChangesAsync();

            return NoContent();
        }

        public bool RepliesExists(int id)
        {
            return _dbc.Replies.Any(e => e.RepliesId == id);
        }
    }
}

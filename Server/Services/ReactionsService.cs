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

    public class ReactionsService : ControllerBase, IReactionsService
    {
        private readonly DBContext _dbc;

        public ReactionsService(DBContext dbc)
        {
            _dbc = dbc;
        }

        public async Task<ActionResult<List<Reactions>>> GetReactions()
        {
            var items = await _dbc.Reactions.ToListAsync();

            if (items == null)
            {
                return NotFound();
            }

            return items;
        }

        public async Task<ActionResult<Reactions>> GetReactionsById(int id)
        {
            var postItem = await _dbc.Reactions.FindAsync(id);

            if (postItem == null)
            {
                return NotFound();
            }

            return postItem;
            //return await _dbc.Reactions.Where(x => x.ReactionsId == id).FirstOrDefaultAsync();
        }

        public async Task<ActionResult<Reactions>> CreateReactions(Reactions reactions)
        {
            _dbc.Reactions.Add(reactions);
            await _dbc.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReactions), reactions);
        }

        public async Task<IActionResult> UpdateReactionsById(int id, Reactions reactions)
        {
            if (id != reactions.ReactionsId)
            {
                return BadRequest();
            }

            _dbc.Entry(reactions).State = EntityState.Modified;

            try
            {
                await _dbc.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReactionsExists(id))
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

        public async Task<IActionResult> DeleteReactionsById(int id)
        {
            var postItem = await _dbc.Reactions.FindAsync(id);
            if (postItem == null)
            {
                return NotFound();
            }

            _dbc.Reactions.Remove(postItem);
            await _dbc.SaveChangesAsync();

            return NoContent();
        }

        public bool ReactionsExists(int id)
        {
            return _dbc.Reactions.Any(e => e.ReactionsId == id);
        }
    }
}

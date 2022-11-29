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

    public class FeedbacksService : ControllerBase, IFeedbacksService
    {
        private readonly DBContext _dbc;

        public FeedbacksService(DBContext dbc)
        {
            _dbc = dbc;
        }

        public async Task<ActionResult<List<Feedback>>> GetFeedbacks()
        {
            var items = await _dbc.Feedbacks.ToListAsync();

            if (items == null)
            {
                return NotFound();
            }

            return items;
        }

        public async Task<ActionResult<Feedback>> GetFeedbacksById(int id)
        {
            var FeedbackItem = await _dbc.Feedbacks.FindAsync(id);

            if (FeedbackItem == null)
            {
                return NotFound();
            }

            return FeedbackItem;
            //return await _dbc.Feedbacks.Where(x => x.FeedbacksId == id).FirstOrDefaultAsync();
        }

        public async Task<ActionResult<Feedback>> CreateFeedbacks(Feedback Feedbacks)
        {
            _dbc.Feedbacks.Add(Feedbacks);
            await _dbc.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFeedbacks), Feedbacks);
        }

        public async Task<IActionResult> UpdateFeedbacksById(int id, Feedback Feedbacks)
        {
            if (id != Feedbacks.FeedbackId)
            {
                return BadRequest();
            }

            _dbc.Entry(Feedbacks).State = EntityState.Modified;

            try
            {
                await _dbc.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbacksExists(id))
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

        public async Task<IActionResult> DeleteFeedbacksById(int id)
        {
            var FeedbackItem = await _dbc.Feedbacks.FindAsync(id);
            if (FeedbackItem == null)
            {
                return NotFound();
            }

            _dbc.Feedbacks.Remove(FeedbackItem);
            await _dbc.SaveChangesAsync();

            return NoContent();
        }

        public bool FeedbacksExists(int id)
        {
            return _dbc.Feedbacks.Any(e => e.FeedbackId == id);
        }
    }
}

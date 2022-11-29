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

    public class AttachmentsService : ControllerBase, IAttachmentsService
    {
        private readonly DBContext _dbc;

        public AttachmentsService(DBContext dbc)
        {
            _dbc = dbc;
        }

        public async Task<ActionResult<List<Attachments>>> GetAttachments()
        {
            var items = await _dbc.Attachments.ToListAsync();

            if (items == null)
            {
                return NotFound();
            }

            return items;
        }

        public async Task<ActionResult<Attachments>> GetAttachmentsById(int id)
        {
            var postItem = await _dbc.Attachments.FindAsync(id);

            if (postItem == null)
            {
                return NotFound();
            }

            return postItem;
            //return await _dbc.Attachments.Where(x => x.AttachmentsId == id).FirstOrDefaultAsync();
        }

        public async Task<ActionResult<Attachments>> CreateAttachments(Attachments attachments)
        {
            _dbc.Attachments.Add(attachments);
            await _dbc.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAttachments), attachments);
        }

        public async Task<IActionResult> UpdateAttachmentsById(int id, Attachments attachments)
        {
            if (id != attachments.AttachsId)
            {
                return BadRequest();
            }

            _dbc.Entry(attachments).State = EntityState.Modified;

            try
            {
                await _dbc.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttachmentsExists(id))
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

        public async Task<IActionResult> DeleteAttachmentsById(int id)
        {
            var postItem = await _dbc.Attachments.FindAsync(id);
            if (postItem == null)
            {
                return NotFound();
            }

            _dbc.Attachments.Remove(postItem);
            await _dbc.SaveChangesAsync();

            return NoContent();
        }

        public bool AttachmentsExists(int id)
        {
            return _dbc.Attachments.Any(e => e.AttachsId == id);
        }
    }
}

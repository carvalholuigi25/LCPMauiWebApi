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

    public class PostsService : ControllerBase, IPostsService
    {
        private readonly DBContext _dbc;

        public PostsService(DBContext dbc)
        {
            _dbc = dbc;
        }

        public async Task<ActionResult<List<Posts>>> GetPosts()
        {
            var items = await _dbc.Posts.ToListAsync();

            if (items == null)
            {
                return NotFound();
            }

            return items;
        }

        public async Task<ActionResult<Posts>> GetPostsById(int id)
        {
            var postItem = await _dbc.Posts.FindAsync(id);

            if (postItem == null)
            {
                return NotFound();
            }

            return postItem;
            //return await _dbc.Posts.Where(x => x.PostsId == id).FirstOrDefaultAsync();
        }

        public async Task<ActionResult<Posts>> CreatePosts(Posts posts)
        {
            _dbc.Posts.Add(posts);
            await _dbc.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPosts), posts);
        }

        public async Task<IActionResult> UpdatePostsById(int id, Posts posts)
        {
            if (id != posts.PostsId)
            {
                return BadRequest();
            }

            _dbc.Entry(posts).State = EntityState.Modified;

            try
            {
                await _dbc.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostsExists(id))
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

        public async Task<IActionResult> DeletePostsById(int id)
        {
            var postItem = await _dbc.Posts.FindAsync(id);
            if (postItem == null)
            {
                return NotFound();
            }

            _dbc.Posts.Remove(postItem);
            await _dbc.SaveChangesAsync();

            return NoContent();
        }

        public bool PostsExists(int id)
        {
            return _dbc.Posts.Any(e => e.PostsId == id);
        }
    }
}

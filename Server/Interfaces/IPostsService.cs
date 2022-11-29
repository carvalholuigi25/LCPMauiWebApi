using LCPMauiWebApi.Server.Classes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCPMauiWebApi.Server.Interfaces
{
    public interface IPostsService
    {
        public Task<ActionResult<List<Posts>>> GetPosts();
        public Task<ActionResult<Posts>> GetPostsById(int id);
        public Task<ActionResult<Posts>> CreatePosts(Posts posts);
        public Task<IActionResult> UpdatePostsById(int id, Posts posts);
        public Task<IActionResult> DeletePostsById(int id);
        public bool PostsExists(int id);
    }
}

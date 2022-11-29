using LCPMauiWebApi.Server.Classes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCPMauiWebApi.Server.Interfaces
{
    public interface ICommentsService
    {
        public Task<ActionResult<List<Comments>>> GetComments();
        public Task<ActionResult<Comments>> GetCommentsById(int id);
        public Task<ActionResult<Comments>> CreateComments(Comments comments);
        public Task<IActionResult> UpdateCommentsById(int id, Comments comments);
        public Task<IActionResult> DeleteCommentsById(int id);
        public bool CommentsExists(int id);
    }
}

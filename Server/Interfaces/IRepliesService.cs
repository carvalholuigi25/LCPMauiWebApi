using LCPMauiWebApi.Server.Classes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCPMauiWebApi.Server.Interfaces
{
    public interface IRepliesService
    {
        public Task<ActionResult<List<Replies>>> GetReplies();
        public Task<ActionResult<Replies>> GetRepliesById(int id);
        public Task<ActionResult<Replies>> CreateReplies(Replies replies);
        public Task<IActionResult> UpdateRepliesById(int id, Replies replies);
        public Task<IActionResult> DeleteRepliesById(int id);
        public bool RepliesExists(int id);
    }
}

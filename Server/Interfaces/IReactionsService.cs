using LCPMauiWebApi.Server.Classes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCPMauiWebApi.Server.Interfaces
{
    public interface IReactionsService
    {
        public Task<ActionResult<List<Reactions>>> GetReactions();
        public Task<ActionResult<Reactions>> GetReactionsById(int id);
        public Task<ActionResult<Reactions>> CreateReactions(Reactions reactions);
        public Task<IActionResult> UpdateReactionsById(int id, Reactions reactions);
        public Task<IActionResult> DeleteReactionsById(int id);
        public bool ReactionsExists(int id);
    }
}

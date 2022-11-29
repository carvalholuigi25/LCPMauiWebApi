using LCPMauiWebApi.Server.Classes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCPMauiWebApi.Server.Interfaces
{
    public interface IFeedbacksService
    {
        public Task<ActionResult<List<Feedback>>> GetFeedbacks();
        public Task<ActionResult<Feedback>> GetFeedbacksById(int id);
        public Task<ActionResult<Feedback>> CreateFeedbacks(Feedback Feedbacks);
        public Task<IActionResult> UpdateFeedbacksById(int id, Feedback Feedbacks);
        public Task<IActionResult> DeleteFeedbacksById(int id);
        public bool FeedbacksExists(int id);
    }
}

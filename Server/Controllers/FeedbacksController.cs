using Microsoft.AspNetCore.Mvc;
using LCPMauiWebApi.Server.Classes;
using LCPMauiWebApi.Server.Interfaces;

namespace LCPMauiWebApi.Server.Controllers
{
    [Route("api/feedbacks")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbacksService _iFeedbackssrv;
        private readonly ILogger _logger;

        public FeedbacksController(IFeedbacksService iFeedbackssrv, ILogger logger)
        {
            _iFeedbackssrv = iFeedbackssrv;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Feedback>>> GetFeedbacks()
        {
            _logger.LogInformation("Get all feedbacks executing...");
            return await _iFeedbackssrv.GetFeedbacks();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Feedback>> GetFeedbacks(int id)
        {
            _logger.LogInformation("Get feedbacks by id executing...");
            return await _iFeedbackssrv.GetFeedbacksById(id);
        }

        // To protect from overFeedbacking attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedbacks(int id, Feedback Feedbacks)
        {
            _logger.LogInformation("Update feedbacks by id executing...");
            return await _iFeedbackssrv.UpdateFeedbacksById(id, Feedbacks);
        }

        // To protect from overFeedbacking attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Feedback>> PostFeedbacks(Feedback Feedbacks)
        {
            _logger.LogInformation("Create feedbacks executing...");
            return await _iFeedbackssrv.CreateFeedbacks(Feedbacks);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedbacks(int id)
        {
            _logger.LogInformation("Delete feedbacks by id executing...");
            return await _iFeedbackssrv.DeleteFeedbacksById(id);
        }
    }
}

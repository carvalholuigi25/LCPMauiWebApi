using Microsoft.AspNetCore.Mvc;
using LCPMauiWebApi.Server.Classes;
using LCPMauiWebApi.Server.Interfaces;

namespace LCPMauiWebApi.Server.Controllers
{
    [Route("api/attachments")]
    [ApiController]
    public class AttachmentsController : ControllerBase
    {
        private readonly IAttachmentsService _iattachmentssrv;
        private readonly ILogger _logger;

        public AttachmentsController(IAttachmentsService iattachmentssrv, ILogger logger)
        {
            _iattachmentssrv = iattachmentssrv;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Attachments>>> GetAttachments()
        {
            _logger.LogInformation("Get all attachments executing...");
            return await _iattachmentssrv.GetAttachments();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Attachments>> GetAttachments(int id)
        {
            _logger.LogInformation("Get attachments by id executing...");
            return await _iattachmentssrv.GetAttachmentsById(id);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttachments(int id, Attachments attachments)
        {
            _logger.LogInformation("Update attachments by id executing...");
            return await _iattachmentssrv.UpdateAttachmentsById(id, attachments);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Attachments>> PostAttachments(Attachments attachments)
        {
            _logger.LogInformation("Create attachments executing...");
            return await _iattachmentssrv.CreateAttachments(attachments);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttachments(int id)
        {
            _logger.LogInformation("Delete attachments executing...");
            return await _iattachmentssrv.DeleteAttachmentsById(id);
        }
    }
}

using LCPMauiWebApi.Server.Classes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCPMauiWebApi.Server.Interfaces
{
    public interface IAttachmentsService
    {
        public Task<ActionResult<List<Attachments>>> GetAttachments();
        public Task<ActionResult<Attachments>> GetAttachmentsById(int id);
        public Task<ActionResult<Attachments>> CreateAttachments(Attachments attachments);
        public Task<IActionResult> UpdateAttachmentsById(int id, Attachments attachments);
        public Task<IActionResult> DeleteAttachmentsById(int id);
        public bool AttachmentsExists(int id);
    }
}

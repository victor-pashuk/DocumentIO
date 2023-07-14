// Controllers/DocumentController.cs

using DocumentIO.API.Utility;
using DocumentIO.Application.Interfaces.Services;
using DocumentIO.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace DocumentIO.API.Controllers
{
    [ApiController]
    [Route("api/documents")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Document>> GetDocumentById(int id)
        {
            var document = await _documentService.GetDocumentByIdAsync(id);
            if (document == null)
            {
                return NotFound();
            }
            return document;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateDocument(IFormFile file, [FromForm] string name, [FromForm] string type, [FromForm] int creatorId)
        {
            var documentId = await _documentService.UploadDocumentAsync(name, type, await file.ToArray(), creatorId);
            return CreatedAtAction(nameof(GetDocumentById), new { id = documentId }, documentId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocument(int id, [FromBody] Document document)
        {
            if (id != document.Id)
            {
                return BadRequest();
            }

            await _documentService.UpdateDocumentAsync(document);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            await _documentService.DeleteDocumentAsync(id);
            return NoContent();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocumentsByUserId(int userId)
        {
            var documents = await _documentService.GetDocumentsByUserIdAsync(userId);
            return Ok(documents);
        }

        [HttpPost("{id}/download")]
        public async Task<IActionResult> DownloadDocument(int id)
        {
            var document = await _documentService.GetDocumentByIdAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            // Perform download logic here
            // e.g., return the document file as a download response

            return Ok();
        }
    }
}

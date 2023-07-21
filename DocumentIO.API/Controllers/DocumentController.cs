
using DocumentIO.API.Utility;
using DocumentIO.Application.Interfaces.Services;
using DocumentIO.Application.Utility;
using DocumentIO.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace DocumentIO.API.Controllers
{
    [ApiController]
    [Route("api/documents")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly IFileStorageService _fileStorageService;

        public DocumentController(IDocumentService documentService, IFileStorageService fileStorageService)
        {
            _documentService = documentService;
            _fileStorageService = fileStorageService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Document>> GetDocumentById(long id)
        {
            var document = await _documentService.GetDocumentByIdAsync(id);
            if (document == null)
            {
                return NotFound();
            }
            return document;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateDocument(IFormFile file, [FromForm] string name, [FromForm] string type, [FromForm] long creatorId)
        {

            var fileName = $"{name}.{type}";
            var fileGuid = await _fileStorageService.UploadFileAsync(fileName, await file.ToArrayAsync());


            var documentId = await _documentService.CreateDocumentAsync(name, type, fileGuid, creatorId);

            return CreatedAtAction(nameof(GetDocumentById), new { id = documentId }, documentId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocument(long id, [FromBody] Document document)
        {
            if (id != document.Id)
            {
                return BadRequest();
            }

            await _documentService.UpdateDocumentAsync(document);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(long id)
        {
            await _documentService.DeleteDocumentAsync(id);
            return NoContent();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocumentsByUserId(long userId)
        {
            var documents = await _documentService.GetDocumentsByUserIdAsync(userId);
            return Ok(documents);
        }

        [HttpGet("{id}/download")]
        public async Task<IActionResult> DownloadDocument(long id)
        {
            var document = await _documentService.GetDocumentByIdAsync(id);
            if (document == null)
            {
                return NotFound();
            }
            var fileName = $"{document.Name}.{document.Type}";

            var fileData = await _fileStorageService.DownloadFileAsync(document.FileGuid);
            if (fileData == null)
            {
                return NotFound();
            }
            await _documentService.IncrementDownloadCountAsync(document.Id);
            var contentType = ContentTypeHelper.GetContentType(fileName);

            return File(fileData, contentType, fileName);
        }

    }
}

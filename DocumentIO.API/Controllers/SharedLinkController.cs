using System.IO;
using System.Reflection.Metadata;
using DocumentIO.Application.Interfaces.Services;
using DocumentIO.Application.Services;
using DocumentIO.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DocumentIO.API.Controllers
{
    [Route("api/[controller]")]
    public class SharedLinkController : Controller
    {
        private readonly ISharedLinkService _sharedLinkService;

        public SharedLinkController(ISharedLinkService sharingLinkService)
        {
            _sharedLinkService = sharingLinkService;
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<SharedLink>> GetSharingLinkById(long id)
        {
            var sharingLink = await _sharedLinkService.GetSharedLinkByIdAsync(id);
            if (sharingLink == null)
            {
                return NotFound();
            }
            return sharingLink;
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<string>> CreateSharedLink([FromForm] long documentId, [FromForm] int expirationHours)
        {
            var sharedLink = await _sharedLinkService.CreateSharedLinkAsync(documentId, expirationHours);
            var shortURL = WebEncoders.Base64UrlEncode(BitConverter.GetBytes(sharedLink.Id));
            var result = $"{this.HttpContext.Request.Scheme}://{this.HttpContext.Request.Host}/{shortURL}";
            return CreatedAtAction(nameof(GetSharingLinkById), new { id = sharedLink.Id }, result);
        }

        [HttpGet("{shortedURL}/download")]
        public async Task<IActionResult> DownloadDocumentBySharedLink(string shortedURL)
        {
            var id = BitConverter.ToInt32(WebEncoders.Base64UrlDecode(shortedURL));
            var isValid = await _sharedLinkService.ValidateSharedLinkAsync(id);
            if (isValid)
            {
                var sharedLink = await _sharedLinkService.GetSharedLinkByIdAsync(id);
                return RedirectToAction("DownloadDocument", "Document", new { id = sharedLink.DocumentId });
            }
            return NotFound();

        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}


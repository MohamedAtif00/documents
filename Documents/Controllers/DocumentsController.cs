using Documents.Data;
using Documents.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Documents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly DocumentDbContext _context;

        public DocumentsController(DocumentDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllDocuments")]
        public async Task<ActionResult<IEnumerable<Document>>> GetAllDocuments()
        {

            var documents = await _context.Documents.ToListAsync();

            return Ok(documents);
        }

        // GET: api/Documents?status=Reviewed
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocuments([FromQuery] string status)
        {
            var documents = string.IsNullOrEmpty(status)
                ? await _context.Documents.ToListAsync()
                : await _context.Documents.Where(d => d.Status == status).ToListAsync();

            return Ok(documents);
        }

        // GET: api/Documents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Document>> GetDocument(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            document.Status = document.Status;

            await _context.SaveChangesAsync();

            if (document == null)
            {
                return NotFound();
            }

            return Ok(document);
        }
        [HttpGet("{id}/file")]
        public IActionResult GetDocumentFile(int id)
        {
            // Find the document by id in the database
            var document = _context.Documents.Find(id);

            if (document == null)
            {
                return NotFound("Document not found.");
            }

            // Construct the full file path (assuming FilePath contains the relative path)
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", document.FilePath);

            // Check if the file exists
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found.");
            }

            // Get the file as a byte array
            var fileBytes = System.IO.File.ReadAllBytes(filePath);

            // Get the MIME type (assuming it's a PDF)
            var mimeType = "application/pdf"; // Adjust according to file type

            // Return the file using the File() method
            return File(fileBytes, mimeType, Path.GetFileName(filePath));
        }




        // PUT: api/Documents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocument(Guid id, [FromBody] Document updatedDocument)
        {
            if (id != updatedDocument.Id)
            {
                return BadRequest();
            }

            var document = await _context.Documents.FindAsync(id);

            if (document == null)
            {
                return NotFound();
            }

            document.Status = updatedDocument.Status;
            document.Comments = updatedDocument.Comments;
            document.SignedDate = updatedDocument.Status == "Signed" ? DateTime.Now : null;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Documents
        [HttpPost]
        public async Task<ActionResult<Document>> AddDocument([FromForm] AddDocumentDto documentDto)
        {
            // Add the document to the database (without the file path yet)
            Document document = new Document() { Name = documentDto.name,Status=documentDto.status};
            _context.Documents.Add(document);
            await _context.SaveChangesAsync();  // This will generate the document ID

            // Define the path where the file will be saved (using the document ID as the filename)
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "documents", $"{document.Id}{Path.GetExtension(documentDto.document.FileName)}");

            // Create the "documents" folder inside wwwroot if it doesn't exist
            var directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Save the file in the wwwroot/documents folder
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await documentDto.document.CopyToAsync(stream);
            }

            // Update the document's FilePath to store the relative path to the file
            document.FilePath = Path.Combine("documents", $"{document.Id}{Path.GetExtension(documentDto.document.FileName)}");
            await _context.SaveChangesAsync();  // Save the updated document with the file path

            // Return the created document response
            return CreatedAtAction(nameof(GetDocument), new { id = document.Id }, document);
        }



        // DELETE: api/Documents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var document = await _context.Documents.FindAsync(id);

            if (document == null)
            {
                return NotFound();
            }

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPost("{documentId}/add-comment")]
        public IActionResult AddComment(int documentId, [FromBody] AddCommentRequest request)
        {
            // Find the document by id
            var document = _context.Documents.Find(documentId);
            if (document == null)
            {
                return NotFound("Document not found.");
            }

            // Find the user (assuming the userId is passed in the request)
            var user = _context.Users.Find(request.UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Create the new comment and associate it with the document and user
            var comment = new Comment
            {
                Content = request.Comment,
                DocumentId = documentId,
                UserId = request.UserId,
                CreatedAt = DateTime.UtcNow
            };

            // Add the comment to the database
            _context.Comments.Add(comment);

            // Change the document status to "Signed"
            document.Status = "Signed";

            // Save changes
            _context.SaveChanges();

            return Ok(new { message = "Comment added and document status changed to Signed." });
        }
    }



}
public record AddDocumentDto(string name,string status,IFormFile document);

public class AddCommentRequest
{
    public string Comment { get; set; }
    public Guid UserId { get; set; } // The ID of the user adding the comment
}


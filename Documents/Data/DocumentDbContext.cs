using Documents.Model;
using Microsoft.EntityFrameworkCore;

namespace Documents.Data
{
    public class DocumentDbContext : DbContext
    {
        public DbSet<Document> Documents { get; set; }

        public DocumentDbContext(DbContextOptions<DocumentDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>().HasData(
                new Document { Id = 1, Name = "Document1.pdf", Status = "Reviewed", FilePath = "/docs/Document1.pdf",Comments =new List<string> { "comment" } },
                new Document { Id = 2, Name = "Document2.pdf", Status = "Signed", FilePath = "/docs/Document2.pdf",Comments= new List<string> { "comment" } },
                new Document { Id = 3, Name = "Document3.pdf", Status = "Hold", FilePath = "/docs/Document3.pdf",Comments= new List<string> { "comment" } }
            );
        }
    }
}

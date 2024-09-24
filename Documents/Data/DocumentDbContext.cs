using Documents.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Documents.Data
{
    public class DocumentDbContext : IdentityDbContext<IdentityUser<Guid>,IdentityRole<Guid>,Guid>
    {
        public DbSet<Document> Documents { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DocumentDbContext(DbContextOptions<DocumentDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>().HasData(
                new Document { Id = Guid.NewGuid(), Name = "Document1.pdf", Status = "Reviewed", FilePath = "/docs/Document1.pdf"},
                new Document { Id = Guid.NewGuid(), Name = "Document2.pdf", Status = "Signed", FilePath = "/docs/Document2.pdf"},
                new Document { Id = Guid.NewGuid(), Name = "Document3.pdf", Status = "Hold", FilePath = "/docs/Document3.pdf"}
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}

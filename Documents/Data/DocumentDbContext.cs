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


            base.OnModelCreating(modelBuilder);
        }
    }
}

namespace Documents.Model
{
    public class Comment
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        // Foreign key to the User entity
        public Guid UserId { get; set; }

        // Foreign key to the Document entity
        public Guid DocumentId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}

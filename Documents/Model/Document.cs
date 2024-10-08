﻿namespace Documents.Model
{
    public class Document
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; } // e.g., "Reviewed", "Signed", "Hold"
        public string? FilePath { get; set; } // Path to the PDF file
        public DateTime? SignedDate { get; set; } // Nullable in case it's not signed yet
        public ICollection<Comment>? Comments { get; set; }
    }

}

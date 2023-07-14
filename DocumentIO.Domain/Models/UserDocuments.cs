namespace DocumentIO.Domain.Models
{
    public class UserDocuments
    {
        public long Id { get; set; }

        public long UserId { get; set; }
        public User? User { get; set; }

        public List<Document>? Documents { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace DocumentIO.Domain.Models
{
    public class Document
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public byte[] Data { get; set; }

        public long CreatorId { get; set; }

        public User? Creator { get; set; }

        public DateTime UploadDateTime { get; set; }

        public int DownloadCount { get; set; }

        public Document(string name, string type, byte[] data, long creatorId)
        {
            Name = name;
            Type = type;
            Data = data;
            CreatorId = creatorId;
            UploadDateTime = DateTime.UtcNow;
            DownloadCount = 0;
        }
    }
}


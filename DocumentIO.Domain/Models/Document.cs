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
        public string FileGuid { get; set; }

        public long CreatorId { get; set; }

        public User? Creator { get; set; }

        public DateTime UploadDateTime { get; set; }

        public int DownloadCount { get; set; }

        public Document(string name, string type, string fileGuid, long creatorId)
        {
            Name = name;
            Type = type;
            FileGuid = fileGuid;
            CreatorId = creatorId;
            UploadDateTime = DateTime.UtcNow;
            DownloadCount = 0;
        }
    }
}


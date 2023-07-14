using System.ComponentModel.DataAnnotations;

namespace DocumentIO.Domain.Models
{
    public class SharingLink
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public required string Token { get; set; }

        [Required]
        public DateTime ExpirationDateTime { get; set; }

        [Required]
        public long DocumentId { get; set; }

        public Document? Document { get; set; }
    }
}


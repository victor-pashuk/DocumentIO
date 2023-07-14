using System.ComponentModel.DataAnnotations;

namespace DocumentIO.Domain.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public required string Username { get; set; }

        [Required]
        public required string Password { get; set; }

        public UserDocuments? UserDocuments { get; set; }

    }
}


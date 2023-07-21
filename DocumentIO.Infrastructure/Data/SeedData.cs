using DocumentIO.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;
using System.Text;

namespace DocumentIO.Infrastructure.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DocumentDbContext(serviceProvider.GetRequiredService<DbContextOptions<DocumentDbContext>>()))
            {
                // Check if there are any existing users
                if (context.Users.Any())
                {
                    return; // Database has already been seeded
                }

                // Seed the default users with passwords
                var users = new[]
                {
                    new User { Username = "John", Password = HashPassword("john123") },
                    new User { Username = "Alex", Password = HashPassword("Alex456") }
                };

                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }

        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using DocumentIO.Domain.Models;

namespace DocumentIO.Infrastructure.Data
{
    public class DocumentDbContext : DbContext
    {
        public DocumentDbContext(DbContextOptions<DocumentDbContext> options)
            : base(options)
        {
        }

        public DbSet<Document> Documents { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserDocuments> UserDocuments { get; set; }
        public DbSet<SharingLink> SharingLinks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Document>().ToTable("Documents");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<UserDocuments>().ToTable("UserDocuments");
            modelBuilder.Entity<SharingLink>().ToTable("SharingLinks");

            modelBuilder.Entity<User>()
                .HasOne(u => u.UserDocuments)
                .WithOne(ud => ud.User)
                .HasForeignKey<UserDocuments>(ud => ud.UserId);

            modelBuilder.Entity<Document>()
                .HasOne(d => d.Creator)
                .WithMany()
                .HasForeignKey(d => d.CreatorId);
        }
    }
}

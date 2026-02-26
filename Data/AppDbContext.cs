using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<UserEntity> users { get; set; }
        public DbSet<MessageEntity> Messages { get; set; }


       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(256);

                entity.HasIndex(u => u.Email)
                      .IsUnique();

                entity.Property(u => u.PasswordHash)
                      .IsRequired();

                entity.Property(u => u.Role)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(u => u.CreatedAt)
                      .HasDefaultValueSql("GETUTCDATE()");

                entity.HasIndex(u => u.RefreshTokenHash)
                      .IsUnique()
                      .HasFilter("[RefreshTokenHash] IS NOT NULL");

                entity.Property(u => u.RefreshTokenExpiresAt);


                entity.HasIndex(u => u.RefreshTokenHash)
                      .IsUnique();

                entity.HasQueryFilter(u => !u.IsDeleted);
            });

            modelBuilder.Entity<MessageEntity>(e =>
            {
                e.HasKey(u => u.Id);
               
                e.Property(m => m.Message).IsRequired().HasMaxLength(500);

                e.Property(m => m.SentAt).IsRequired();

                e.HasOne(m => m.Sender).WithMany().HasForeignKey(m => m.SenderId).OnDelete(DeleteBehavior.Restrict);

                e.HasOne(m => m.Receiver).WithMany().HasForeignKey(m => m.ReceiverId).OnDelete(DeleteBehavior.Restrict);


            });
        }
    }
}

using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)  : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<MessageEntity> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureUser(modelBuilder);
            ConfigureMessage(modelBuilder);
        }

        private void ConfigureUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.ToTable("Users");

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

                entity.Property(u => u.RefreshTokenHash)
                      .HasMaxLength(500);

                entity.HasIndex(u => u.RefreshTokenHash)
                      .IsUnique()
                      .HasFilter("[RefreshTokenHash] IS NOT NULL");

                entity.Property(u => u.RefreshTokenExpiresAt);

                entity.HasQueryFilter(u => !u.IsDeleted);

              
            });
        }

        private void ConfigureMessage(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageEntity>(entity =>
            {
                entity.ToTable("Messages");

                entity.HasKey(m => m.Id);

                entity.Property(m => m.Message)
                      .IsRequired()
                      .HasMaxLength(500);

                entity.Property(m => m.SentAt)
                      .IsRequired();

                entity.HasIndex(m => m.SenderId);
                entity.HasIndex(m => m.ReceiverId);
                entity.HasIndex(m => m.SentAt);

                entity.HasOne(m => m.Sender)
                      .WithMany()
                      .HasForeignKey(m => m.SenderId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(m => m.Receiver)
                      .WithMany()
                      .HasForeignKey(m => m.ReceiverId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
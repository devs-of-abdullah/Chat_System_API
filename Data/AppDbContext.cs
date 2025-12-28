using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<MessageEntity> Messages { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           modelBuilder.Entity<UserEntity>(e => {

               e.HasKey(u => u.Id);

               e.Property(u => u.Username).IsRequired().HasMaxLength(50);

               e.Property(e => e.Email).IsRequired().HasMaxLength(100);

               e.HasIndex(u => u.Email).IsUnique();
           });

            modelBuilder.Entity<MessageEntity>(e =>
            {
                e.HasKey(u => u.Id);
               
                e.Property(m => m.Content).IsRequired().HasMaxLength(500);

                e.Property(m => m.SentAt).IsRequired();

                e.HasOne(m => m.Sender).WithMany().HasForeignKey(m => m.SenderId).OnDelete(DeleteBehavior.Restrict);

                e.HasOne(m => m.Receiver).WithMany().HasForeignKey(m => m.ReceiverId).OnDelete(DeleteBehavior.Restrict);


            });
        }
    }
}

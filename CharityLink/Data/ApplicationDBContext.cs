using CharityLink.Models;
using Microsoft.EntityFrameworkCore;

namespace CharityLink.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions) 
        { 

        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Community> Communities { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Donation> Donations  { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserCommunity> UserCommunities { get; set; }
        public DbSet<Message> Messages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserCommunity>()
            .HasKey(uc => new { uc.UserId, uc.CommunityId });

            // Cấu hình quan hệ cho Like như ban đầu
            modelBuilder.Entity<Like>()
                .HasOne(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(l => l.PostId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Donation>()
            .HasOne(d => d.Donor)
            .WithMany(u => u.Donations)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Donation>()
            .HasOne(d => d.Community)
            .WithMany(c => c.Donations)
            .HasForeignKey(d => d.CommunityId)
            .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Post>()
        .HasOne(p => p.Author)
        .WithMany(u => u.Posts)
        .HasForeignKey(p => p.UserId)
        .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Post>()
        .HasOne(p => p.Community)
        .WithMany(c => c.Posts)
        .HasForeignKey(p => p.CommunityID)
        .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<UserCommunity>()
        .HasOne(uc => uc.User)
        .WithMany(u => u.UserCommunities)
        .HasForeignKey(uc => uc.UserId)
        .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<UserCommunity>()
    .HasOne(uc => uc.Community)
    .WithMany(c => c.UserCommunities)
    .HasForeignKey(uc => uc.CommunityId)
    .OnDelete(DeleteBehavior.Cascade);


         

            modelBuilder.Entity<Donation>()
        .Property(d => d.Amount)
        .HasColumnType("decimal(18,2)");


            modelBuilder.Entity<Comment>()
         .HasOne(c => c.ParentComment) // Quan hệ đến bình luận cha
         .WithMany(c => c.Replies) // Quan hệ đến danh sách bình luận con
         .HasForeignKey(c => c.ParentCommentId) // Khóa ngoại đến bình luận cha
         .OnDelete(DeleteBehavior.NoAction); // Tắt cascade delete


            modelBuilder.Entity<Comment>()
    .HasOne(c => c.Post) // Quan hệ đến bài viết
    .WithMany(p => p.Comments) // Quan hệ ngược lại
    .HasForeignKey(c => c.PostId)
    .OnDelete(DeleteBehavior.NoAction); // Tắt cascade delete

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Author) // Quan hệ đến người dùng
                .WithMany(u => u.Comments) // Quan hệ ngược lại
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction); // Tắt cascade delete
        }

        


    }
}

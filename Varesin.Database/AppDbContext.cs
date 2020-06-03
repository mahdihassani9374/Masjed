using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Varesin.Database.Identity.Entities;
using Varesin.Domain.Entities;

namespace Varesin.Database
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<SlideShow> SlideShows { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Info> Infoes { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<LogService> LogServices { get; set; }
        public DbSet<InstagramTag> InstagramTags { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostFile> PostFiles { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsFile> NewsFiles { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventFile> EventFiles { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var user = builder.Entity<User>();

            user
                .Property(c => c.FullName)
                .HasMaxLength(500)
                .IsRequired(true);

            var slideShow = builder.Entity<SlideShow>();

            slideShow.HasKey(c => c.Id);
            slideShow.Property(c => c.Title).HasMaxLength(128).IsRequired(true);
            slideShow.Property(c => c.Description).HasMaxLength(128).IsRequired(false);
            slideShow.Property(c => c.Link).HasMaxLength(128).IsRequired(false);

            var payment = builder.Entity<Payment>();
            payment.HasKey(c => c.Id);
            payment.Property(c => c.FullName).HasMaxLength(300);
            payment.Property(c => c.PhoneNumber).HasMaxLength(100);

            var info = builder.Entity<Info>();
            info.HasKey(c => c.Id);
            info.Property(c => c.Name).HasMaxLength(200);

            var contactUs = builder.Entity<ContactUs>();
            contactUs.HasKey(c => c.Id);
            contactUs.Property(c => c.FullName).HasMaxLength(200);
            contactUs.Property(c => c.PhoneNumber).HasMaxLength(200);
            contactUs.Property(c => c.Text).HasMaxLength(3000);

            var log = builder.Entity<LogService>();
            log.HasKey(c => c.Id);
            log.Property(c => c.Method).HasMaxLength(100);
            log.Property(c => c.RelativePath).HasMaxLength(12000);
            log.Property(c => c.UserId).HasMaxLength(320);

            var instaTag = builder.Entity<InstagramTag>();
            instaTag.HasKey(c => c.Id);
            instaTag.Property(c => c.Name).HasMaxLength(400).IsRequired(true);


            var post = builder.Entity<Post>();

            post.HasKey(c => c.Id);

            post.Property(c => c.Title)
                .HasMaxLength(128)
                .IsRequired(true);

            post.Property(c => c.PrimaryPicture)
               .HasMaxLength(128)
               .IsRequired(true);

            post.Property(c => c.Description)
               .IsRequired(false);

            var postFile = builder.Entity<PostFile>();

            postFile.HasKey(c => c.Id);
            postFile.Property(c => c.FileName).HasMaxLength(128).IsRequired(true);
            postFile.Property(c => c.Title).HasMaxLength(128).IsRequired(true);

            postFile.HasOne(c => c.Post).WithMany(c => c.Files).HasForeignKey(c => c.PostId).OnDelete(DeleteBehavior.Cascade);

            var news = builder.Entity<News>();

            news.HasKey(c => c.Id);

            news.Property(c => c.Title)
                .HasMaxLength(128)
                .IsRequired(true);

            news.Property(c => c.PrimaryPicture)
               .HasMaxLength(128)
               .IsRequired(true);

            news.Property(c => c.Description)
               .IsRequired(false);

            var newsFile = builder.Entity<NewsFile>();

            newsFile.HasKey(c => c.Id);
            newsFile.Property(c => c.FileName).HasMaxLength(128).IsRequired(true);
            newsFile.Property(c => c.Title).HasMaxLength(128).IsRequired(true);

            newsFile.HasOne(c => c.News).WithMany(c => c.Files).HasForeignKey(c => c.NewsId).OnDelete(DeleteBehavior.Cascade);

            var eventEntity = builder.Entity<Event>();

            eventEntity.HasKey(c => c.Id);

            eventEntity.Property(c => c.Title)
                .HasMaxLength(128)
                .IsRequired(true);

            eventEntity.Property(c => c.PrimaryPicture)
               .HasMaxLength(128)
               .IsRequired(true);

            eventEntity.Property(c => c.Description)
               .IsRequired(false);

            var eventFile = builder.Entity<EventFile>();

            eventFile.HasKey(c => c.Id);
            eventFile.Property(c => c.FileName).HasMaxLength(128).IsRequired(true);
            eventFile.Property(c => c.Title).HasMaxLength(128).IsRequired(true);

            eventFile.HasOne(c => c.Event).WithMany(c => c.Files).HasForeignKey(c => c.EventId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}

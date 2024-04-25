using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyStoreDB"));
        }

        #region
        public virtual DbSet<Tab> Tabs { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Post_Comment> Post_Comments { get; set; }
        public virtual DbSet<Post_Like> Post_Likes { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Users>()
                .HasMany(c => c.Post_Comments)
                .WithOne(p => p.Users)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Users>()
                .HasMany(c => c.Post_Likes)
                .WithOne(p => p.Users)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Posts>()
                .HasMany(c => c.Post_Comments)
                .WithOne(p => p.Posts)
                .HasForeignKey(p => p.PostId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Posts>()
                .HasMany(c => c.Post_Likes)
                .WithOne(p => p.Posts)
                .HasForeignKey(p => p.PostId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity
            <Role>().HasData(
            new Role { role_id = 1, roleName = "Admin" },
            new Role { role_id = 2, roleName = "Blogger" },
            new Role { role_id = 3, roleName = "User" }
            );
            builder.Entity
            <Category>().HasData(
            new Category { category_id = 1, categoryName = "Digital Marketing", published = 1 },
            new Category { category_id = 2, categoryName = "SEO", published = 1 },
            new Category { category_id = 3, categoryName = "Web Design", published = 1 }
            );
            builder.Entity
            <Tab>().HasData(
            new Tab { tab_id = 1, tabName = "Social Media Marketing", published = 1 },
            new Tab { tab_id = 2, tabName = "Video Marketing", published = 1 },
            new Tab { tab_id = 3, tabName = "Content Marketing", published = 1 },
            new Tab { tab_id = 4, tabName = "Local SEO", published = 1 },
            new Tab { tab_id = 5, tabName = "User Experience", published = 1 },
            new Tab { tab_id = 6, tabName = "SEO Basics", published = 1 },
            new Tab { tab_id = 7, tabName = "Web Development", published = 1 },
            new Tab { tab_id = 8, tabName = "Webpage Design", published = 1 },
            new Tab { tab_id = 9, tabName = "WordPress Website", published = 1 }
            );
        }
    }
}

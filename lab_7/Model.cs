using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace lab_7
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public string ConnectionString { get; }

        public BloggingContext(string connectionString) => ConnectionString = connectionString;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(this.ConnectionString);
        }
    }

    public class Blog
    {
        public long Id { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; } = new();
    }

    public class Post
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public long BlogId { get; set; }
        public Blog Blog { get; set; }
    }

    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public int RoleID { get; set; }
    }

    public class Role
    {
        public int ID { get; set; }
        public string RoleName { get; set; }
    }

    public class Task
    {
        public int ID { get; set; }
        public string TaskName { get; set; }
        public int UserID { get; set; }
    }

}
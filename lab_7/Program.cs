using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace lab_7
{
    public class Program
    {
        public static string connectionString = @"Data Source=localhost;Initial Catalog=blogdb;Integrated Security=True";

        public static async System.Threading.Tasks.Task PerformDataBaseOperations()
        {
            using (BloggingContext db = new BloggingContext(connectionString))
            {
                Console.WriteLine("Adding roles");
                db.Add(new Role { RoleName = "admin", ID = 1 });
                db.Add(new Role { RoleName = "moderator", ID = 2 });
                db.Add(new Role { RoleName = "user", ID = 3 });

                Console.WriteLine("Calling SaveChanges.");
                await db.SaveChangesAsync();
                Console.WriteLine("SaveChanges completed.");


                Role admin = db.Roles.Where(r => r.ID == 1).First();
                Role moderator = db.Roles.Where(r => r.RoleName == "moderator").First();
                Role user = db.Roles.Where(r => r.RoleName == "user").First();

                Console.WriteLine("Adding users");
                db.Add(new User { ID = 1, Username = "adminUser", RoleID = 1 });
                db.Add(new User { ID = 2, Username = "moderatorUser", RoleID = 2 });
                db.Add(new User { ID = 3, Username = "userUser", RoleID = 3 });

                Console.WriteLine("Calling SaveChanges.");
                await db.SaveChangesAsync();
                Console.WriteLine("SaveChanges completed.");


                User adminUser = db.Users.Where(u => u.Username == "adminUser").First();
                User moderatorUser = db.Users.Where(u => u.Username == "moderatorUser").First();
                User userUser = db.Users.Where(u => u.Username == "userUser").First();

                Console.WriteLine("Adding tasks");
                db.Add(new Task { ID = 10, TaskName = "task1", UserID = 3 });
                db.Add(new Task { ID = 20, TaskName = "task2", UserID = 3 });
                db.Add(new Task { ID = 30, TaskName = "task3", UserID = 3 });

                Console.WriteLine("Calling SaveChanges.");
                await db.SaveChangesAsync();
                Console.WriteLine("SaveChanges completed.");

                

                Task task1 = db.Tasks.Where(t => t.ID == 10).First();
                Task task2 = db.Tasks.Where(t => t.ID == 20).First();
                Task task3 = db.Tasks.Where(t => t.ID == 30).First();

                Console.WriteLine("Updating records");
                userUser.Username = "changedUser";
                userUser.RoleID = 2;

                Console.WriteLine("Calling SaveChanges.");
                await db.SaveChangesAsync();
                Console.WriteLine("SaveChanges completed.");

                Console.WriteLine($"Updated user {userUser.Username} with ID {userUser.ID} role ID changed to {userUser.RoleID}");

                Console.WriteLine("Deleting users");
                db.Remove(adminUser);
                db.Remove(moderatorUser);
                db.Remove(userUser);
                Console.WriteLine("Calling SaveChanges.");
                await db.SaveChangesAsync();
                Console.WriteLine("SaveChanges completed.");

                Console.WriteLine("Deleting tasks");
                db.Remove(task1);
                db.Remove(task2);
                db.Remove(task3);
                Console.WriteLine("Calling SaveChanges.");
                await db.SaveChangesAsync();
                Console.WriteLine("SaveChanges completed.");

                Console.WriteLine("Deleting roles");
                db.Remove(admin);
                db.Remove(moderator);
                db.Remove(user);
                Console.WriteLine("Calling SaveChanges.");
                await db.SaveChangesAsync();
                Console.WriteLine("SaveChanges completed.");
            }
        }


        public static async System.Threading.Tasks.Task Main()
        {
            // Hint: change `DESKTOP-123ABC\SQLEXPRESS` to your server name
            //       alternatively use `localhost` or `localhost\SQLEXPRESS`





            using (BloggingContext db = new BloggingContext(connectionString))
            {
                Console.WriteLine($"Database ConnectionString: {db.ConnectionString}.");

                // Create
                Console.WriteLine("Inserting a new blog");

                db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
                db.SaveChanges();

                // Read
                Console.WriteLine("Querying for a blog");

                Blog blog = db.Blogs
                    .OrderBy(b => b.Id)
                    .First();

                // Update
                Console.WriteLine("Updating the blog and adding a post");

                blog.Url = "https://devblogs.microsoft.com/dotnet";
                blog.Posts.Add(new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
                db.SaveChanges();

                // Delete
                Console.WriteLine("Delete the blog");

                db.Remove(blog);
                db.SaveChanges();



            }

            await PerformDataBaseOperations();
        }
    }
}
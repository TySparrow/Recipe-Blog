using Microsoft.EntityFrameworkCore;
using Recipe_Blog.Web.Models.Domain;

namespace Recipe_Blog.Web.Data
{
    public class Recipe_BlogDbContext : DbContext
    {
        //Constructor 
        public Recipe_BlogDbContext(DbContextOptions options) : base(options)
        {

        }

        //Creates two base tables in database 
        public DbSet<BlogPost> BlogPosts { get; set; }

        public DbSet<Tag> Tags { get; set; }
    }
}

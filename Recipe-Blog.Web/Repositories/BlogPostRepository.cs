using Microsoft.EntityFrameworkCore;
using Recipe_Blog.Web.Data;
using Recipe_Blog.Web.Models.Domain;

namespace Recipe_Blog.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly Recipe_BlogDbContext dbContext;

        public BlogPostRepository(Recipe_BlogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await dbContext.BlogPosts.AddAsync(blogPost);

            await dbContext.SaveChangesAsync();

            return blogPost;
        }

        public Task<BlogPost?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync()
        {
            return await dbContext.BlogPosts.ToListAsync();
        }

        public async Task<BlogPost?> GetBlogPostByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            throw new NotImplementedException();
        }
    }
}

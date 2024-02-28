using Recipe_Blog.Web.Models.Domain;

namespace Recipe_Blog.Web.Repositories
{
    public interface IBlogPostRepository
    {
        //5 methods for all CRUD operations

        Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync();

        Task<BlogPost?> GetBlogPostByIdAsync(Guid id);

        Task<BlogPost> AddAsync(BlogPost blogPost);

        Task<BlogPost?> UpdateAsync(BlogPost blogPost);

        Task<BlogPost?> DeleteAsync(Guid id);
    }
}

using Recipe_Blog.Web.Models.Domain;

namespace Recipe_Blog.Web.Repositories
{
    public interface ITagRepository
    {
        //5 methods needed for all CRUD operations. Async added.

        Task<IEnumerable<Tag>> GetAllTagsAsync();

        Task<Tag?> GetTagByIdAsync(Guid id);

        Task<Tag> AddAsync(Tag tag);

        Task<Tag?> UpdateAsync(Tag tag);

        Task<Tag?> DeleteAsync(Guid id);

    }
}

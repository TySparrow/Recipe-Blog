using Microsoft.EntityFrameworkCore;
using Recipe_Blog.Web.Data;
using Recipe_Blog.Web.Models.Domain;
using Recipe_Blog.Web.Models.ViewModels;

namespace Recipe_Blog.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly Recipe_BlogDbContext dbContext;
        public TagRepository(Recipe_BlogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            //Calling dbContext to access Tags Sql table asyncronously
            await dbContext.Tags.AddAsync(tag);

            //Saves changes to database asyncronously
            await dbContext.SaveChangesAsync();

            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await dbContext.Tags.FindAsync(id);

            if (existingTag != null)
            {
                dbContext.Tags.Remove(existingTag);

                await dbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
            return await dbContext.Tags.ToListAsync();

        }

        public Task<Tag?> GetTagByIdAsync(Guid id)
        {
            //var tag = dbContext.Tags.Find(id); (Another way to find by id)
            return dbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await dbContext.Tags.FindAsync(tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                //Save Changes
                await dbContext.SaveChangesAsync();

                return existingTag;
            }
            return null;
        }
    }
}

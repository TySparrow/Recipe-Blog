using Microsoft.AspNetCore.Mvc;
using Recipe_Blog.Web.Models.ViewModels;
using Recipe_Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Recipe_Blog.Web.Models.Domain;

namespace Recipe_Blog.Web.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogPostRepository;

        public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //Get Tags from repository
            var tags = await tagRepository.GetAllTagsAsync();

            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            //Map view model to domain model 
            var blogPost = new BlogPost

            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                Description = addBlogPostRequest.Description,
                ImageUrl = addBlogPostRequest.ImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishedDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible,
                
            };
            //Maps Tags from selected tags
            //New instance of a list of type Tag
            var selectedTags = new List<Tag>();

            //Loop through each tagId in the request, if they are found, add to the list.
            foreach(var tagId in addBlogPostRequest.SelectedTags)
            {
                var selectedTagId = Guid.Parse(tagId);
                var existingTag = await tagRepository.GetTagByIdAsync(selectedTagId);
                if(existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }
            //Found tags from the loop are then added to the blogPost that is to be added
            blogPost.Tags = selectedTags;

            await blogPostRepository.AddAsync(blogPost);
            return RedirectToAction("Add");
        }
        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            //Calling dbContext to generate the list of tags
            var blogPosts = await blogPostRepository.GetAllBlogPostsAsync();

            return View(blogPosts);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Recipe_Blog.Web.Models.ViewModels;
using Recipe_Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Recipe_Blog.Web.Models.Domain;
using System.Reflection.Metadata.Ecma335;

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
            //Calling repository to generate the list of blog posts
            var blogPosts = await blogPostRepository.GetAllBlogPostsAsync();

            return View(blogPosts);
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            //Pull result from repository
            var blogPost = await blogPostRepository.GetBlogPostByIdAsync(id);

            var tags = await tagRepository.GetAllTagsAsync();

            if(blogPost != null)
            {
                //Map domain model into view model
                var viewModel = new EditBlogPostRequest
                {
                    Id = blogPost.Id,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    Content = blogPost.Content,
                    Author = blogPost.Author,
                    ImageUrl = blogPost.ImageUrl,
                    UrlHandle = blogPost.UrlHandle,
                    Description = blogPost.Description,
                    PublishedDate = blogPost.PublishedDate,
                    Visible = blogPost.Visible,
                    Tags = tags.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray()
                };
                //Return data to view (edit page)
                return View(viewModel);
            }
            else
            {
                return View(null);
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPost)
        {
            //Map view model back to domain model
            var domainModel = new BlogPost
            {
                Id = editBlogPost.Id,
                Heading = editBlogPost.Heading,
                PageTitle = editBlogPost.PageTitle,
                Content = editBlogPost.Content,
                Author = editBlogPost.Author,
                ImageUrl = editBlogPost.ImageUrl,
                UrlHandle = editBlogPost.UrlHandle,
                Description = editBlogPost.Description,
                PublishedDate = editBlogPost.PublishedDate,
                Visible = editBlogPost.Visible
            };
            //Map tags into domain model
            var selectedTags = new List<Tag>();

            foreach(var selectedTag in editBlogPost.SelectedTags)
            {
                if(Guid.TryParse(selectedTag, out var tag))
                {
                    var foundTag = await tagRepository.GetTagByIdAsync(tag);
                        if(foundTag != null)
                    {
                        selectedTags.Add(foundTag);
                    }
                }
            }
            domainModel.Tags = selectedTags;


            //Submit changes to repository to update database
            var updatedBlog = await blogPostRepository.UpdateAsync(domainModel);

            //Redirect view to GET
            if(updatedBlog != null)
            {
                //Show success notification
                return RedirectToAction("Edit");
            }
            else
            {
                //Show error notification
                return RedirectToAction("Edit");
            }
        }
        
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recipe_Blog.Web.Data;
using Recipe_Blog.Web.Models.Domain;
using Recipe_Blog.Web.Models.ViewModels;
using Recipe_Blog.Web.Repositories;

namespace Recipe_Blog.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            //Mapping domain model to add and save to database using DbContext
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            await tagRepository.AddAsync(tag);

            //When a new tag is added, redirected to table with all tags
            return RedirectToAction("List");
        }
        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            //Calling dbContext to generate the list of tags
            var tags = await tagRepository.GetAllTagsAsync();

            return View(tags);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {

            var tag = await tagRepository.GetTagByIdAsync(id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTagRequest);
            }
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var updatedTag = await tagRepository.UpdateAsync(tag);
            if(updatedTag != null)
            {
                //show success notification
            }
            else
            {
                //Show failure/error notification
            }

            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {

            var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);

            if(deletedTag != null)
            {
                //Show success notification
                return RedirectToAction("List");
            }

            //Show failure notification to admin user
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
    }
}

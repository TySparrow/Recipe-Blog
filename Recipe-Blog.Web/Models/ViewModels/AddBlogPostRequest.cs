using Microsoft.AspNetCore.Mvc.Rendering;

namespace Recipe_Blog.Web.Models.ViewModels
{
    public class AddBlogPostRequest
    {
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }

        //Display tags
        public IEnumerable<SelectListItem> Tags { get; set; }

        //Capture collection of tags (many to many relationship posts-tags)
        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
}

namespace NetBlog.WebApplication.Pages.Blog
{
    using EnaBricks.Generics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using NetBlog.Queries.Blogging;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class ArchivesModel : PageModel
    {
        private readonly BlogEntryQueryComponent _queryComponent;
        public BlogEntryTextModel Blog { get; set; }
        public string Id { get; set; }

        public ArchivesModel(BlogEntryQueryComponent queryComponent)
        {
            _queryComponent = queryComponent ?? throw new ArgumentNullException(nameof(queryComponent));
        }

        public async Task<ActionResult> OnGetAsync(string id)
        {
            Id = id;

            if (string.IsNullOrWhiteSpace(id))
                return RedirectToPage("/NotFound");

            Option<BlogEntryTextModel> blogEntryOption = await _queryComponent.BlogAsync(id);

            if (!blogEntryOption.Any())
                return RedirectToPage("/NotFound");

            Blog = blogEntryOption.Single();

            return Page();
        }
    }
}
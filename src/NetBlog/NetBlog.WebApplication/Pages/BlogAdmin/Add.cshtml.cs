namespace NetBlog.WebApplication.Pages.BlogAdmin
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using NetBlog.Domain.Blogging.Component;
    using NetBlog.Queries.Blogging;
    using NetBlog.Requests;
    using NetBlog.SharedFramework;
    using System;
    using System.Threading.Tasks;

    public class AddModel : PageModel
    {
        private readonly BlogEntryQueryComponent _queryComponent;

        [BindProperty]
        public AddUpdateBlogEntry BlogEntryUpdateRequest { get; set; }

        public AddModel(BlogEntryQueryComponent queryComponent)
        {
            _queryComponent = queryComponent ?? throw new ArgumentNullException(nameof(queryComponent));
        }

        public void OnGet()
        {

        }

        public async Task<ActionResult> OnPostAsync([FromServices]BlogEntryComponentFactory blogEntryComponentFactory)
        {
            if (blogEntryComponentFactory == null)
            {
                throw new ArgumentNullException(nameof(blogEntryComponentFactory));
            }

            if (!ModelState.IsValid)
                return Page();

            var component = blogEntryComponentFactory.New();

            WorkflowResult result = await component.Create(
                BlogEntryUpdateRequest.UriKey,
                BlogEntryUpdateRequest.Title,
                BlogEntryUpdateRequest.MinutesToRead,
                BlogEntryUpdateRequest.TextIntro,
                BlogEntryUpdateRequest.TextEntry,
                BlogEntryUpdateRequest.KeyWordId
            );

            if (!result.Success)
            {
                for (int i = 0; i < result.Errors.Length; i++)
                {
                    ModelState.AddModelError("", result.Errors[i]);
                }
                return Page();
            }

            return RedirectToPage("./Edit", new { id = BlogEntryUpdateRequest.UriKey });
        }
    }
}
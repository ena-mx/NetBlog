namespace NetBlog.WebApplication.Pages.BlogAdmin
{
    using EnaBricks.Generics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using NetBlog.Domain.Blogging.Component;
    using NetBlog.Queries.Blogging;
    using NetBlog.Requests;
    using NetBlog.SharedFramework;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class EditModel : PageModel
    {
        private readonly BlogEntryQueryComponent _queryComponent;

        [BindProperty]
        public string Id { get; set; }

        [BindProperty]
        public AddUpdateBlogEntry BlogEntryUpdateRequest { get; set; }

        public EditModel(BlogEntryQueryComponent queryComponent)
        {
            _queryComponent = queryComponent ?? throw new ArgumentNullException(nameof(queryComponent));
        }

        public async Task<ActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return RedirectToPage("/NotFound");

            Id = id;
            return await BindPageModelAndRedirect();
        }

        public async Task<ActionResult> OnPostAsync([FromServices]BlogEntryComponentFactory blogEntryComponentFactory)
        {
            if (blogEntryComponentFactory == null)
            {
                throw new ArgumentNullException(nameof(blogEntryComponentFactory));
            }

            if (!ModelState.IsValid)
                return Page();

            Option<BlogEntryTextModel> blogEntryOption = await _queryComponent.BlogAsync(Id);

            if (!blogEntryOption.Any())
                return RedirectToPage("/NotFound");

            BlogEntryTextModel Blog = blogEntryOption.Single();

            Option<BlogEntryComponent> componentResult = await blogEntryComponentFactory.LoadComponentAsynd(Blog.BlogEntryId);

            if (!componentResult.Any())
                return RedirectToPage("/NotFound");

            BlogEntryComponent component = componentResult.Single();

            WorkflowResult result = await component.Update(
                BlogEntryUpdateRequest.UriKey,
                BlogEntryUpdateRequest.Title,
                BlogEntryUpdateRequest.MinutesToRead,
                BlogEntryUpdateRequest.TextIntro,
                BlogEntryUpdateRequest.TextEntry,
                BlogEntryUpdateRequest.KeyWordId
            );

            if(!result.Success)
            {
                for (int i = 0; i < result.Errors.Length; i++)
                {
                    ModelState.AddModelError("", result.Errors[i]);
                }
            }
            else
            {
                Id = BlogEntryUpdateRequest.UriKey;
            }

            return await BindPageModelAndRedirect(true, !result.Success);
        }

        private async Task<ActionResult> BindPageModelAndRedirect(bool isPost = false, bool isError = false)
        {
            Option<BlogEntryTextModel> blogEntryOption = await _queryComponent.BlogAsync(Id);

            if (!blogEntryOption.Any())
                return RedirectToPage("/NotFound");

            BlogEntryTextModel Blog = blogEntryOption.Single();

            BlogEntryUpdateRequest = Blog.MapToAddUpdateBlogEntryRequest();

            if (isPost && !isError)
                return RedirectToPage("/Blog/Index");
            return Page();
        }
    }
}
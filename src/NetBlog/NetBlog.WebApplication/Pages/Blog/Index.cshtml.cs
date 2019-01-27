namespace NetBlog.WebApplication.Pages.Blog
{
    using EnaBricks.Generics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using NetBlog.Queries.Blogging;
    using NetBlog.Queries.Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class BlogIndexModelComposite
    {
        public GenericPage<BlogEntryModel> Blogs { get; set; }
        public List<SelectListItem> PageLimitItems { get; set; }
        public int Limit { get; set; }
        public int PageIndex { get; set; }
        public int Offset { get; set; }
        public int PageLimit { get; set; }
        public string RouteName { get; set; }
    }

    public class IndexModel : PageModel
    {
        private readonly BlogEntryQueryComponent _queryComponent;
        public BlogIndexModelComposite BlogModel { get; set; } = new BlogIndexModelComposite();


        public IndexModel(BlogEntryQueryComponent queryComponent)
        {
            _queryComponent = queryComponent ?? throw new ArgumentNullException(nameof(queryComponent));
        }

        public async Task<ActionResult> OnGetAsync(int pageIndex = 1, int pageLimit = 10)
        {
            if(pageIndex <= 0 || pageLimit <= 0)
                return RedirectToPage("/NotFound");
            BlogModel.Limit = pageLimit;
            BlogModel.PageIndex = pageIndex;
            BlogModel.Offset = (BlogModel.PageIndex - 1) * BlogModel.Limit;
            Option<GenericPage<BlogEntryModel>> blogsOption = await _queryComponent.BlogsAsync(BlogModel.Offset, BlogModel.Limit);
            if (!blogsOption.Any())
                return RedirectToPage("/NotFound");
            BlogModel.Blogs = blogsOption.Single();
            BindPageLimitSelectItems();
            return Page();
        }

        private void BindPageLimitSelectItems()
        {
            BlogModel.RouteName = "./Index";
            BlogModel.PageLimitItems = new List<SelectListItem>(4) {
                new SelectListItem { Value = "10", Text = Resources.GridResources.Grid_TenRows, Selected = (10 == BlogModel.Limit) },
                new SelectListItem { Value = "20", Text = Resources.GridResources.Grid_TwentyRows, Selected = (20 == BlogModel.Limit) },
                new SelectListItem { Value = "30", Text = Resources.GridResources.Grid_ThirtyRows, Selected = (30 == BlogModel.Limit) },
                new SelectListItem { Value = "50", Text = Resources.GridResources.Grid_FiftyRows, Selected = (50 == BlogModel.Limit) }
            };
        }
    }
}
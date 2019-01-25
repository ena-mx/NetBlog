namespace NetBlog.WebApplication.Pages.Blog
{
    using EnaBricks.Generics;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using NetBlog.Queries.Blogging;
    using NetBlog.Queries.Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class IndexModel : PageModel
    {
        private readonly BlogEntryQueryComponent _queryComponent;
        public GenericPage<BlogEntryModel> Blogs { get; set; }
        public List<SelectListItem> PageLimitItems { get; set; }
        public int Limit { get; set; }
        public int PageIndex { get; set; }
        public int Offset { get; set; }
        public int PageLimit { get; set; }

        public IndexModel(BlogEntryQueryComponent queryComponent)
        {
            _queryComponent = queryComponent ?? throw new ArgumentNullException(nameof(queryComponent));
        }

        public async Task OnGetAsync(int pageIndex = 1, int pageLimit = 10)
        {
            Limit = pageLimit;
            PageIndex = pageIndex;
            Offset = (PageIndex - 1) * Limit;
            BindPageLimitSelectItems();
            Option<GenericPage<BlogEntryModel>> blogsOption = await _queryComponent.BlogsAsync(Offset, Limit);
            if (blogsOption.Any())
                Blogs = blogsOption.Single();
        }

        private void BindPageLimitSelectItems()
        {
            PageLimitItems = new List<SelectListItem>(4) {
                new SelectListItem { Value = "10", Text = Resources.GridResources.Grid_TenRows, Selected = (10 == Limit) },
                new SelectListItem { Value = "20", Text = Resources.GridResources.Grid_TwentyRows, Selected = (20 == Limit) },
                new SelectListItem { Value = "30", Text = Resources.GridResources.Grid_ThirtyRows, Selected = (30 == Limit) },
                new SelectListItem { Value = "50", Text = Resources.GridResources.Grid_FiftyRows, Selected = (50 == Limit) }
            };
        }
    }
}
namespace NetBlog.WebApplication.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using NetBlog.Domain.Blogging;
    using NetBlog.Queries.Blogging;
    using System.Linq;
    using System.Threading.Tasks;

    public class PageHeaderViewComponent : ViewComponent
    {
        private readonly BlogConfig _blogConfig;

        public PageHaderModel PageHeaderData { get; set; } = new PageHaderModel();

        public PageHeaderViewComponent(BlogConfig blogConfig)
        {
            _blogConfig = blogConfig ?? throw new System.ArgumentNullException(nameof(blogConfig));
        }

        public async Task<IViewComponentResult> InvokeAsync(string title)
        {
            PageHeaderData.Title = title;
            var profileUrlOption = await _blogConfig.GetProfilePicUrl();
            if (!profileUrlOption.Any())
                PageHeaderData.ProfileUrl = string.Empty;
            else
                PageHeaderData.ProfileUrl = profileUrlOption.Single();
            return View(PageHeaderData);
        }
    }
}

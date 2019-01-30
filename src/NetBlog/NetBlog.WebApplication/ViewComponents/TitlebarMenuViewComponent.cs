namespace NetBlog.WebApplication.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using NetBlog.Domain.Blogging;
    using NetBlog.Queries.Menu;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using EnaBricks.Generics;

    public class TitlebarMenuViewComponent : ViewComponent
    {
        private readonly BlogConfig _blogConfig;
        private readonly MenuQueryComponent _menuQueryComponent;

        public TitlebarMenuViewComponent(BlogConfig blogConfig, MenuQueryComponent menuQueryComponent)
        {
            _blogConfig = blogConfig ?? throw new System.ArgumentNullException(nameof(blogConfig));
            _menuQueryComponent = menuQueryComponent ?? throw new System.ArgumentNullException(nameof(menuQueryComponent));
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Option<string> blogName = await _blogConfig.GetBlogNameAsync();
            MenuItem[] menuItems = await _menuQueryComponent.GetTitlebarMenuItemsAsync() ?? Array.Empty<MenuItem>();
            KeyValuePair<string, MenuItem[]> modelData = new KeyValuePair<string, MenuItem[]>(blogName.FirstOrDefault() ?? string.Empty, menuItems);
            return View(modelData);
        }
    }
}

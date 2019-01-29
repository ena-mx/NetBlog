namespace NetBlog.WebApplication.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using NetBlog.Queries.Menu;
    using System.Threading.Tasks;

    public class LeftMenuViewComponent : ViewComponent
    {
        private readonly MenuQueryComponent _menuQueryComponent;

        public LeftMenuViewComponent(MenuQueryComponent menuQueryComponent)
        {
            _menuQueryComponent = menuQueryComponent ?? throw new System.ArgumentNullException(nameof(menuQueryComponent));
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            MenuItem[] menuItems = await _menuQueryComponent.GetMenuItems();
            return View(menuItems);
        }
    }
}

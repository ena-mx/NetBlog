namespace NetBlog.WebApplication.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class ProfileViewComponent : ViewComponent
    {
        //public async Task<IViewComponentResult> InvokeAsync()
        //{
        //    return View(users);
        //}

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}

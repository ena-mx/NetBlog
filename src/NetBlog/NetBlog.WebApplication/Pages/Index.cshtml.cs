namespace NetBlog.WebApplication.Pages
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        public ActionResult OnGet()
        {
            return RedirectToPage("/Blog/Index");
        }
    }
}
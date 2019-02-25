namespace NetBlog.WebApplication.Pages
{
    using EnaBricks.Generics;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using NetBlog.Domain.Blogging.Component;
    using System.Linq;
    using System.Threading.Tasks;

    [AllowAnonymous]
    public class WhoIAmModel : PageModel
    {
        private readonly BlogConfig _blogConfig;
        public string HtmlData { get; set; }


        public WhoIAmModel(BlogConfig blogConfig)
        {
            _blogConfig = blogConfig;
        }

        public async Task<ActionResult> OnGetAsync()
        {
            Option<string> HtmlDataOption = await _blogConfig.GetWhoIAmHtml();
            if (!HtmlDataOption.Any())
                return RedirectToPage("/NotFound");
            HtmlData = HtmlDataOption.Single();
            return Page();
        }
    }
}
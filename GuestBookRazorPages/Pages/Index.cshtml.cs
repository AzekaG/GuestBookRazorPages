using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GuestBookRazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public Task<IActionResult> OnGet()
        {
            if(!String.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
            {
               return Task.FromResult<IActionResult>(RedirectToPage("./FeedBackPage"));
            }
            return Task.FromResult<IActionResult>(Page());
        }
    }
}

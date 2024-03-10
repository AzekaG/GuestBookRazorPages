using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GuestBookRazorPages.Pages
{
    public class LogOutModel : PageModel
    {
        public Task<IActionResult> OnGet()
        {
            HttpContext.Session.Clear();
            return Task.FromResult<IActionResult>(RedirectToPage("./Index"));
        }
    }
}

using GuestBookRazorPages.Interfaces;
using GuestBookRazorPages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GuestBookRazorPages.Pages
{
    public class FeedBackPageModel : PageModel
    {
        IUnitOfWork repo { get; set; }

        public IList<Message> messages { get; set; } = default!;


        [BindProperty]
        public string? message { get; set; }


        public FeedBackPageModel(IUnitOfWork unitOfWork)
        {
            repo = unitOfWork;
           
        }

        public async Task OnGet()
        {
            messages = await repo.Messages.GetMessageList();
           
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(HttpContext.Session.GetString("UserId") !=null) 
            {
                User? us = (await repo.Users.GetUserList()).FirstOrDefault(x => x.Id == int.Parse(HttpContext.Session.GetString("UserId")!));
                if(us != null) 
                {
                    await repo.Users.AddMessage(new Message() { msg = message, Us = us, date = DateTime.Now });
                    await repo.Save();
                    messages = await repo.Messages.GetMessageList();
                }
            }
            return Page();
        }
    }
}

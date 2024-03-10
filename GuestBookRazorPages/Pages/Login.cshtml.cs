using GuestBookRazorPages.Interfaces;
using GuestBookRazorPages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;

namespace GuestBookRazorPages.Pages
{
    public class LoginModel : PageModel
    {
        IUnitOfWork repo { get; set; }
        [BindProperty]
        public string? email { get; set; }
        [BindProperty]
        public  string? password { get; set; }


        public LoginModel(IUnitOfWork unitOfWork)
        {
            repo = unitOfWork;
        }
        public void OnGet()
        {
            if(!String.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
                {
                     RedirectToPage("./FeedBackPage");
                }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["Pass"] = "";
            if (ModelState.IsValid)
            {


                return await CheckPass(password!, email!);
            }
            
            return Page();
        }


        public async Task<IActionResult> CheckPass(string pass, string email)
        {
            if (ModelState.IsValid)
            {

                var user = (await repo.Users.GetUserList()).Where(x => x.Email == email).FirstOrDefault();
                if (user == null)
                {
                    ViewData["Pass"] = "Незарегистрированый пользователь";
                    return Page();
                }
            
                string? salt = user.Salt;

                 //переводим пароль в байт-массив  
                byte[] password = Encoding.Unicode.GetBytes(salt + pass);

                   //создаем объект для получения средств шифрования  
                var md5 = MD5.Create();

                //    //вычисляем хеш-представление в байтах  
                byte[] byteHash = md5.ComputeHash(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                     hash.Append(string.Format("{0:X2}", byteHash[i]));

                if (user.Password != hash.ToString())
                  {
                    ViewData["Pass"] = "Неверный пароль";
                    return Page();
                  }

                HttpContext.Session.SetString("Login", user.Login!);
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                return RedirectToPage("./FeedBackPage");
            }
            ViewData["Pass"] = "Неверный пароль";
            return Page();
           
        }


    }
}

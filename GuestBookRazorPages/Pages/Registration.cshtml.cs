using GuestBookRazorPages.Interfaces;
using GuestBookRazorPages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace GuestBookRazorPages.Pages
{
    public class RegistrationModel : PageModel
    {
        IUnitOfWork repo { get; set; }
        [BindProperty]
        public string? email { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        public string? Password { get; set; }


        [BindProperty]
        [Compare("Password", ErrorMessage = "Не совпадают пароли")]
        [DataType(DataType.Password)]
        public string? PasswordConfirm { get; set; }

        [BindProperty]
        public string? login { get; set; }

        public RegistrationModel(IUnitOfWork unitOfWork)
        {
            repo = unitOfWork;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["Pass"] = "";
            if (ModelState.IsValid)
            {
                 return await CreateNewUser(Password!, email! , login!);
            }

            return Page();
        }




        public async Task<IActionResult> CreateNewUser(string pass, string email,string login)
        {
            if (ModelState.IsValid)
            {
                var user = (await repo.Users.GetUserList()).Where(x => x.Email == email).FirstOrDefault();
                if (user != null)
                {
                    ViewData["Pass"] = "Такой пользователь уже существует";
                    return Page();
                }

                byte[] saltbuf = new byte[16];

                RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
                randomNumberGenerator.GetBytes(saltbuf);

                StringBuilder sb = new StringBuilder(16);
                for (int i = 0; i < 16; i++)
                    sb.Append(string.Format("{0:X2}", saltbuf[i]));
                string salt = sb.ToString();

                byte[] password = Encoding.Unicode.GetBytes(salt + pass);

                //создаем объект для получения средств шифрования  
                var md5 = MD5.Create();

                //вычисляем хеш-представление в байтах  
                byte[] byteHash = md5.ComputeHash(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                User us = new User();
                us.Password = hash.ToString();
                us.Salt = salt;
                us.Login = login;
                us.Email = email;

                await repo.Users.CreateUser(us);
                await repo.Save();
                return RedirectToPage("./Login");
            }
            ViewData["Pass"] = "Проверьте введенные данные";
            return Page();

        }

        




    }
}
